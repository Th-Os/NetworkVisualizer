using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour {


    public Transform start;
    public Transform target;

    private TrailRenderer trailRenderer;

    private bool isBusy;

	// Use this for initialization
	void Start () {
        isBusy = false;

        trailRenderer = GetComponent<TrailRenderer>();

        transform.position = start.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target.position, 2f * Time.deltaTime);
	}

    private IEnumerator WaitAndShoot(float waitTime)
    {
        isBusy = true;
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            
            


            isBusy = false;
        }

        
    }
}
