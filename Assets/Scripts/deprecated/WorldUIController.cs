using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


//TODO überarbeiten!

/// <summary>
/// http://heliosinteractive.com/scaling-ui-hololens/
/// </summary>
public class WorldUIController : MonoBehaviour {
    /*
    public Transform DevicePanel;

    float currentZ;
    float currentScale;

	// Use this for initialization
	void Start () {
        currentZ = 0;
<<<<<<< HEAD:Assets/Scripts/UI/WorldUIController.cs
        //EventManager.AddHandler(EVNT.NewDevice, SetElementPosition);
=======
        Events.OnDeviceFound += SetElementPosition;
>>>>>>> d9632934877f6c9cac2f4d2f40c1a745da0c4e4e:Assets/Scripts/deprecated/WorldUIController.cs
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

    void SetElementPosition(Transform t)
    {
        transform.position = t.position;
        transform.LookAt(Camera.main.transform);
        /*
        Instantiate(DevicePanel, transform);
        DevicePanel.position = position;
        DevicePanel.GetComponentInChildren<Text>().text = name;
    }
        */
    
}
