using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour {

    public Transform ConnectionLine;
    public Transform Connection;
    public Transform Call;

    public float m_Connection_Speed_Line = 1f;
    public float m_Connection_Speed = 5f;
    public float m_Connection_Smooth_WaitFor = 0.001f;
    public float m_Call_Duration = 5f;

    private List<Transform[]> _transforms;

    // Use this for initialization
    void Start()
    {
        _transforms = new List<Transform[]>();
        Events.OnDrawConnection += OnConnection;
        Events.OnDrawCall += OnCall;
    }

    // Update is called once per frame
    void Update () {
		
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
    IEnumerator StartConnection(Transform start, Transform target)
    {
        if (Add(start, target))
        {
            LineRenderer lr = Instantiate(ConnectionLine, start).GetComponent<LineRenderer>();
            lr.transform.position = start.position;
            lr.SetPosition(0, start.position);
            lr.SetPosition(1, target.position);
            print(lr.GetPosition(1) + " : " + target.position);
            while (!lr.GetPosition(1).Equals(target.position))
            {
                lr.SetPosition(1, Vector3.MoveTowards(lr.GetPosition(1), target.position, m_Connection_Speed_Line * Time.deltaTime));
                yield return new WaitForSeconds(m_Connection_Smooth_WaitFor);
            }
        }

        StartCoroutine(Connect(start, target));
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

    }

    IEnumerator DeviceCall(Transform source)
    {
        Transform call = Instantiate(Call, source);
        call.position = source.position;
        ParticleSystem system = call.GetComponent<ParticleSystem>();
        system.Play();
        yield return new WaitForSeconds(m_Call_Duration);
        system.Stop();
        while(system.IsAlive())
        {

        }
        Destroy(call);
    }

    private bool Add(Transform one, Transform two)
    {
        foreach (Transform[] transforms in _transforms)
        {
            if (transforms[0].name == one.name || transforms[1].name == one.name)
            {
                if (transforms[0].name == two.name || transforms[1].name == two.name)
                {
                    return false;
                }
            }
        }
        _transforms.Add(new Transform[] { one, two });
        return true;
    }
}
