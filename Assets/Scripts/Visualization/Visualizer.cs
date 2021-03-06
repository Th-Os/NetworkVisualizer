﻿using System;
using System.Collections;
using UnityEngine;
using NetworkVisualizer.Data;

namespace NetworkVisualizer.Visual
{
    /// <summary>
    /// The Visualizer presents the visual layer. Lines of connection can be drawn and single connections can be fired.
    /// Additionally a call animation will trigger, when the specific event is fired.
    /// Last but not least, it handles the highlighting of objects.
    /// </summary>
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

        private void OnCall(Transform source, Transform target, string toIp)
        {
            if(source.GetComponent<DeviceAnimator>())
            {
                source.GetComponent<DeviceAnimator>().Call(target, toIp, OnAnswer);
            }
            //if(source != null)
            //    StartCoroutine(DeviceCall(source));
        }

        private void OnAnswer(Transform target, string toIp)
        {
            if (target.GetComponent<DeviceAnimator>())
                target.GetComponent<DeviceAnimator>().Answer(toIp);
        }

        //Establish Connection
        IEnumerator StartConnection(Transform source, Transform target)
        {
            if (DataStore.Instance.AddConnectedDevices(source, target))
            {
                Transform connection = Instantiate(ConnectionLine, Connections);
                connection.GetComponent<DeviceConnection>().Source = source;
                connection.GetComponent<DeviceConnection>().Target = target;
                LineRenderer lr = connection.GetComponent<LineRenderer>();
                lr.transform.position = source.position;
                lr.SetPosition(0, source.position);
                lr.SetPosition(1, target.position);
                lr.material.mainTextureScale = new Vector3(Vector3.Distance(source.position, target.position), 1f, 1f);
                while (!lr.GetPosition(1).Equals(target.position))
                {
                    lr.SetPosition(1, Vector3.MoveTowards(lr.GetPosition(1), target.position, m_Connection_Speed_Line * Time.deltaTime));
                    yield return new WaitForSeconds(m_Connection_Smooth_WaitFor);
                }

                AddColliderToLine(connection, lr);
            }
            DeviceConnection dc = GetDeviceConnection(source, target);
            Transform parent = dc.transform;
            Transform connectionObject = Instantiate(Connection, parent);
            connectionObject.gameObject.GetComponent<DrawConnection>().Init(m_Connection_Duration).Connect(source, target, dc);
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
            col.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            
        }

        private DeviceConnection GetDeviceConnection(Transform source, Transform target)
        {
            foreach (Transform connectionLine in Connections)
            {
                DeviceConnection dc = connectionLine.GetComponent<DeviceConnection>();
                if (dc != null)
                    if (dc.Source.name.Equals(source.name, StringComparison.OrdinalIgnoreCase) && dc.Target.name.Equals(target.name, StringComparison.OrdinalIgnoreCase))
                        return dc;
                    else
                        if (dc.Source.name.Equals(target.name, StringComparison.OrdinalIgnoreCase) && dc.Target.name.Equals(source.name, StringComparison.OrdinalIgnoreCase))
                            return dc;

            }
            return null;
        }

    }
}