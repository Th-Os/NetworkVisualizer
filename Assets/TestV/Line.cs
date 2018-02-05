using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

    public Transform Source;
    public Transform Target;

    public Transform Ball;
    public Transform Connection;

    public float m_Speed_Line = 1f;
    public float m_Speed_Connection = 5f;
    public float m_Smooth_WaitFor = 0.001f;

    private LineRenderer lineRenderer;
    private List<Transform[]> _transforms;

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        _transforms = new List<Transform[]>();

        transform.position = Source.position;

        StartCoroutine(StartConnection(Source, Target));
	}
	
	// Update is called once per frame
	void Update () {

        /*
        lineRenderer.SetPosition(1, Vector3.MoveTowards(lineRenderer.GetPosition(1), target.position, 2f * Time.deltaTime));
        //positions[1] = Vector3.MoveTowards(lineRenderer.GetPosition(1), target.position, 2f * Time.deltaTime);
        //lineRenderer.SetPositions(positions);

        if (lineRenderer.GetPosition(1).Equals(target.position))
        {
            //Ball.position = Vector3.MoveTowards(Ball.position, target.position, 10f * Time.deltaTime);

            AnotherBall.position = Vector3.MoveTowards(AnotherBall.position, target.position, 10f * Time.deltaTime);

            if(AnotherBall.position.Equals(target.position))
            {
                AnotherBall.position = start.position;
            }
        }

           */
    }

    private void OnConnection(Transform source, Transform target)
    {
        StartCoroutine(StartConnection(source, target));
    }

    private bool Add(Transform one, Transform two)
    {
        foreach(Transform[] transforms in _transforms)
        {
            if(transforms[0].name == one.name || transforms[1].name == one.name)
            {
                if(transforms[0].name == two.name || transforms[1].name == two.name)
                {
                    return false;
                }
            }
        }
        _transforms.Add(new Transform[] { one, two });
        return true;
    }

    //Establish Connection
    IEnumerator StartConnection(Transform start, Transform target)
    {
        if (Add(start, target))
        {
            lineRenderer.SetPosition(0, start.position);
            lineRenderer.SetPosition(1, target.position);
            print(lineRenderer.GetPosition(1) + " : " + target.position);
            while (!lineRenderer.GetPosition(1).Equals(target.position))
            {
                lineRenderer.SetPosition(1, Vector3.MoveTowards(lineRenderer.GetPosition(1), target.position, m_Speed_Line * Time.deltaTime));
                yield return new WaitForSeconds(m_Smooth_WaitFor);
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
            conn.position = Vector3.MoveTowards(conn.position, target.position, m_Speed_Connection * Time.deltaTime);
            yield return new WaitForSeconds(m_Smooth_WaitFor);
        }

    }

}
