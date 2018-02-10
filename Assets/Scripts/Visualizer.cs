using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkVisualizer
{
    public class Visualizer : MonoBehaviour
    {

        public Transform ConnectionLine;
        public Transform Connection;
        public Transform Call;

        public float m_Connection_Speed_Line = 1f;
        public float m_Connection_Speed = 5f;
        public float m_Connection_Smooth_WaitFor = 0.001f;
        public float m_Call_Duration = 5f;


        // Use this for initialization
        void Start()
        {
            Events.OnDrawConnection += OnConnection;
            Events.OnDrawCall += OnCall;
            Events.OnHighlight += OnHighlight;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnHighlight(Transform obj)
        {
            Debug.Log("Wants to highlight " + obj.name + " with tag: " + obj.tag);
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
                print(lr.GetPosition(1) + " : " + target.position);
                while (!lr.GetPosition(1).Equals(target.position))
                {
                    lr.SetPosition(1, Vector3.MoveTowards(lr.GetPosition(1), target.position, m_Connection_Speed_Line * Time.deltaTime));
                    yield return new WaitForSeconds(m_Connection_Smooth_WaitFor);
                }
            }

            StartCoroutine(Connect(source, target));
        }

        //Connect
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