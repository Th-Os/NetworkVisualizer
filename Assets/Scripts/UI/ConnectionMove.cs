using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionMove : MonoBehaviour {

    public Vector3 m_Origin;
    public Vector3 m_Target;
    public float m_Speed;

    private bool moving = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(moving)
        {
            if (transform.position != m_Target)
            {
                transform.position = Vector3.MoveTowards(transform.position, m_Target, m_Speed * Time.deltaTime);
            }
        }
       
    }

    public void Move()
    {
        transform.position = m_Origin;
        moving = true;
    }

    public void Connect()
    {

    }
}
