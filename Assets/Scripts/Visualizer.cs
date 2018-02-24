using System.Collections;
using UnityEngine;

namespace NetworkVisualizer
{
    public class Visualizer : MonoBehaviour
    {

        public Transform ConnectionLine;
        public Transform Connection;
        public Transform Call;

        public Material DeviceHighlightMaterial;

        public float m_Connection_Speed_Line = 1f;
        public float m_Connection_Speed = 5f;
        public float m_Connection_Smooth_WaitFor = 0.001f;
        public float m_Connection_Highlight_Width = 1f;
        public float m_Call_Duration = 5f;

        private Material _standardDeviceMaterial;

        // Use this for initialization
        void Start()
        {
            Events.OnDrawConnection += OnConnection;
            Events.OnDrawCall += OnCall;
            Events.OnHighlight += OnHighlight;
            Events.OnHide += OnHide;
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
            StartCoroutine(StartConnection(source, target));
        }

        private void OnCall(Transform source)
        {
            StartCoroutine(DeviceCall(source));
        }

        //Establish Connection
        IEnumerator StartConnection(Transform source, Transform target)
        {
            if (DataStore.Instance.AddConnectedDevices(source,target))
            {
                
                Transform connection = Instantiate(ConnectionLine, source);
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

                CapsuleCollider capsule = GetComponent<CapsuleCollider>();
                capsule.transform.position = Vector3.Lerp(source.position, target.position, 0.5f);
                capsule.direction = 2;
                capsule.radius = lr.startWidth /2;
                capsule.transform.LookAt(target.position);
                capsule.height = (target.position - source.position).magnitude;
            }

            StartCoroutine(Connect(source, target));
        }

        //Fires one connection
        IEnumerator Connect(Transform start, Transform target)
        {

            Transform conn = Instantiate(Connection, start);
            conn.position = start.position;

            while (!conn.position.Equals(target.position))
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
            while (system.IsAlive())
            {

            }
            Destroy(call.gameObject);
        }

    }
}