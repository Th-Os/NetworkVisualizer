using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MonoBehaviour {

    public Transform Target;
    public Transform Canvas;
    public Transform Connection;
    public Vector3 Offset = new Vector3(0f, 4f, 0f);

    private LineRenderer _line;

    private bool _show;

    //Let Canvas look at camera and align transform to the line

	// Use this for initialization
	void Start () {
        _line = GetComponent<LineRenderer>();
        _line.SetPosition(0, transform.position);
        _line.SetPosition(1, Target.position);

        if (Canvas != null)
        {
            Vector3 middle = Vector3.Lerp(transform.position, Target.position, 0.5f);
            Vector3 position = Vector3.Lerp(transform.position, Target.position + Offset, 0.5f);
            Canvas.position = position;
            Debug.Log(middle);
            //float angle = Vector3.Angle(transform.position, Target.transform.position);
            //Debug.Log("Angle: " + angle);

            Canvas.LookAt(Camera.main.transform);

            float degree = 15f;
            Vector3 direction = transform.forward;
            Quaternion quat = Quaternion.AngleAxis(degree, transform.forward);
            direction = quat * direction;
            Debug.Log(direction);
            Canvas.rotation = Quaternion.LookRotation(direction.normalized);

            //Canvas.position = Quaternion.Euler(euler) * position;

            //Canvas.localRotation *= Quaternion.FromToRotation(transform.position, Target.position);

            //maybe not a good idea to look at camera .. if then canvas rotation.y = -180 else -90


        }
        CapsuleCollider capsule = GetComponentInChildren<CapsuleCollider>();
        capsule.transform.position = Vector3.Lerp(transform.position, Target.position, 0.5f);
        capsule.direction = 2;
        capsule.radius = _line.startWidth /2;
        capsule.transform.LookAt(Target.position);
        
        capsule.height = (Target.position - transform.position).magnitude;

        StartCoroutine(Connect(transform, Target));
    }

    IEnumerator Connect(Transform start, Transform target)
    {

        Transform conn = Instantiate(Connection, start);
        conn.position = start.position;

        while (!conn.position.Equals(target.position))
        {
            conn.position = Vector3.MoveTowards(conn.position, target.position, 5f * Time.deltaTime);
            yield return new WaitForSeconds(0.0001f);
        }

        Destroy(conn.gameObject);

    }


    // Update is called once per frame
    void FixedUpdate () {
	}
}
