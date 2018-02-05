using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


//TODO überarbeiten!

/// <summary>
/// http://heliosinteractive.com/scaling-ui-hololens/
/// </summary>
public class WorldUIController : MonoBehaviour {

    public Transform DevicePanel;

    float currentZ;
    float currentScale;

	// Use this for initialization
	void Start () {
        currentZ = 0;
        //EventManager.AddHandler(EVNT.NewDevice, SetElementPosition);
	}
	
	// Update is called once per frame
	void Update () {
		if(currentZ != transform.position.z)
        {
            currentZ = transform.position.z;
            currentScale = currentZ * 0.00415f;
            transform.localScale.Set(currentScale, currentScale, currentScale);
        }

        if(isActiveAndEnabled)
        {
            transform.LookAt(Camera.main.transform);
        }
	}

    void SetElementPosition(string name, Vector3 position)
    {
        transform.position = position;
        transform.LookAt(Camera.main.transform);
        /*
        Instantiate(DevicePanel, transform);
        DevicePanel.position = position;
        DevicePanel.GetComponentInChildren<Text>().text = name;
        */
    }
}
