using System.Collections;
using UnityEngine;

namespace NetworkVisualizer
{
    public class Visualizer : MonoBehaviour
    {

        public Transform Connections;

        public Transform ConnectionLine;
        public Transform Connection;
        public Transform Call;

        public Material DeviceHighlightMaterial;

        public float m_Connection_Speed_Line = 3f;
        public float m_Connection_Speed = 5f;
        public float m_Connection_Smooth_WaitFor = 0.001f;
        public float m_Connection_Highlight_Width = 1f;
        public float m_Call_Duration = 5f;
        public float m_Connection_Duration = 5f;

        private Material _standardDeviceMaterial;

        // Use this for initialization
        void Start()
        {
            EventHandler.OnDrawConnection += OnConnection;
            EventHandler.OnDrawCall += OnCall;
            EventHandler.OnHighlight += OnHighlight;
            EventHandler.OnHide += OnHide;
            EventHandler.OnDestroyVisualization += OnDestroyConnections;
        }

        private void OnDestroyConnections()
        {
            foreach(Transform c in Connections)
            {
                if(c.name.Contains("(Clone)"))
                {
                    Destroy(c.gameObject);
                }
            }
        }

        private void OnHighlight(Transform obj)
        {
            Debug.Log("Visualizer Highlight with null: " + (obj == null));
            if (obj != null)
            {
                if (obj.CompareTag("Device"))
                {
                    Transform shell = obj.transform.Find("Shell");
                    if (shell != null)
                    {
                        Renderer renderer = shell.gameObject.GetComponent<Renderer>();
                        if (_standardDeviceMaterial == null)
                            _standardDeviceMaterial = renderer.material;

                        renderer.material = DeviceHighlightMaterial;
                    }
                }

                if (obj.CompareTag("Connection"))
                {
                    LineRenderer ln = obj.GetComponent<LineRenderer>();
                    if (ln != null)
                    {
                        ln.widthMultiplier += m_Connection_Highlight_Width;
                    }
                }
                if (obj != null)
                    Debug.Log("Wants to highlight " + obj.name + " with tag: " + obj.tag);
            }
        }

        private void OnHide(Transform obj)
        {
            Debug.Log("Visualizer Hide");
            if (obj != null)
            {
                if (obj.CompareTag("Device"))
                {
                    Transform shell = obj.transform.Find("Shell");
                    if (shell != null)
                    {
                        Renderer renderer = shell.gameObject.GetComponent<Renderer>();
                        renderer.material = _standardDeviceMaterial;
                    }
                }

                if (obj.CompareTag("Connection"))
                {
                    LineRenderer ln = obj.GetComponent<LineRenderer>();
                    if (ln != null)
                    {
                        ln.widthMultiplier -= m_Connection_Highlight_Width;
                    }
                }
                Debug.Log("Wants to hide " + obj.name + " with tag: " + obj.tag);
            }
        }

        private void OnConnection(Transform source, Transform target)
        {
            if(source != null && target != null)
                StartCoroutine(StartConnection(source, target));
        }

        private void OnCall(Transform source)
        {
            if(source != null)
                StartCoroutine(DeviceCall(source));
        }

        //Establish Connection
        IEnumerator StartConnection(Transform source, Transform target)
        {
            if (DataStore.Instance.AddConnectedDevices(source, target))
            {
                Debug.Log("first step");
                Transform connection = Instantiate(ConnectionLine, Connections);
                connection.GetComponent<DeviceConnection>().Source = source;
                connection.GetComponent<DeviceConnection>().Target = target;
                LineRenderer lr = connection.GetComponent<LineRenderer>();
                lr.transform.position = source.position;
                lr.SetPosition(0, source.position);
                lr.SetPosition(1, target.position);
                while (!lr.GetPosition(1).Equals(target.position))
                {
                    lr.SetPosition(1, Vector3.MoveTowards(lr.GetPosition(1), target.position, m_Connection_Speed_Line * Time.deltaTime));
                    yield return new WaitForSeconds(m_Connection_Smooth_WaitFor);
                }

                AddColliderToLine(connection, lr);
            }
            Debug.Log("ran through first step of connection");
            Instantiate(Connection, GetDeviceConnection(source,target).transform).GetComponent<DrawConnection>().Init(m_Connection_Duration).Connect(source, target);
            //StartCoroutine(Connect(source, target));
        }

        //Fires one connection
        IEnumerator Connect(Transform start, Transform target)
        {

            Transform conn = Instantiate(Connection, start);
            conn.position = start.position;

            while (conn.position != target.position)
            {
                conn.position = Vector3.MoveTowards(conn.position, target.position, m_Connection_Speed * Time.deltaTime);
                yield return new WaitForSeconds(m_Connection_Smooth_WaitFor);
            }

            Destroy(conn.gameObject);

        }

        IEnumerator DeviceCall(Transform source)
        {
            Transform call = Instantiate(Call, source);
            call.position = source.position;
            ParticleSystem system = call.GetComponent<ParticleSystem>();
            system.Play();
            yield return new WaitForSeconds(m_Call_Duration);
            system.Stop();
            Destroy(call.gameObject);
        }

        private void AddColliderToLine(Transform t, LineRenderer line)
        {

            BoxCollider col = new GameObject("Collider").AddComponent<BoxCollider>();
            col.transform.parent = line.transform;
            Vector3 start = line.GetPosition(0);
            Vector3 target = line.GetPosition(1);
            float lineLength = Vector3.Distance(start, target);
            col.size = new Vector3(lineLength, line.startWidth + 1f, 1f); 
            Vector3 midPoint = Vector3.Lerp(start, target, 0.5f);
            col.transform.position = midPoint;
            float angle = (Mathf.Abs(start.y - target.y) / Mathf.Abs(start.x - target.x));
            if ((start.y < target.y && start.x > target.x) || (target.y < start.y && target.x > start.x))
            {
                angle *= -1;
            }
            angle = Mathf.Rad2Deg * Mathf.Atan(angle);
            col.transform.Rotate(0, 0, angle);
            col.isTrigger = true;
            col.transform.localScale = new Vector3(1f, 1f, 1f);
            
        }

        private DeviceConnection GetDeviceConnection(Transform source, Transform target)
        {
            foreach (Transform connectionLine in Connections)
            {
                DeviceConnection dc = connectionLine.GetComponent<DeviceConnection>();
                if (dc != null)
                    if (dc.Source.name.Equals(source.name) && dc.Target.name.Equals(target.name))
                        return dc;

            }
            return null;
        }

    }
}