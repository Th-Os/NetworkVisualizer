using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkVisualizer.Objects;

public class Content : MonoBehaviour {

    public Data data { get; set; }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Show()
    {
        if (data != null)
            Events.Broadcast(Events.EVENTS.SHOW_DATA, gameObject, data);
        else
            Debug.Log("No Data available for " + gameObject.name);
    }

    public void Hide()
    {

        Events.Broadcast(Events.EVENTS.HIDE_DATA, GetInstanceID());

    }
}
