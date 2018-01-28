using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

    public Transform start;
    public Transform target;

    public Transform Ball;
    public Transform AnotherBall;

    public float m_Speed_Line = 1f;
    public float m_Speed_Connection = 5f;
    public float m_Smooth_WaitFor = 0.001f;

    private LineRenderer lineRenderer;
    private Vector3[] positions;

	// Use this for initialization
	void Start () {
        positions = new Vector3[2];
        lineRenderer = GetComponent<LineRenderer>();
        transform.position = start.position;

        //positions[0] = start.position;
        //positions[1] = start.position;
        //lineRenderer.SetPositions(positions);

        lineRenderer.SetPosition(0, start.position);
        lineRenderer.SetPosition(1, start.position);

        //Ball.position = start.position;
        AnotherBall.position = start.position;

        StartCoroutine(DrawLine());
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

    IEnumerator DrawLine()
    {
        print(lineRenderer.GetPosition(1) + " : " + target.position);
        while (!lineRenderer.GetPosition(1).Equals(target.position))
        { 
            lineRenderer.SetPosition(1, Vector3.MoveTowards(lineRenderer.GetPosition(1), target.position, m_Speed_Line * Time.deltaTime));
            yield return new WaitForSeconds(m_Smooth_WaitFor);
        }

        StartCoroutine(Connect());
    }

    IEnumerator Connect()
    {
        while (!AnotherBall.position.Equals(target.position))
        {
            AnotherBall.position = Vector3.MoveTowards(AnotherBall.position, target.position, m_Speed_Connection * Time.deltaTime);
            yield return new WaitForSeconds(m_Smooth_WaitFor);
        }

    }

}
