using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NetworkVisualizer.Objects;

public class PanelControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Start");
        Device device = new Device("hello", "1.1.1.1", new Position(new Vector3(1f, 2f, 3f)), "content");
        Debug.Log(device.ToString());
        Text[] texts = GetComponentsInChildren<Text>();
        Debug.Log(texts.Length);
        device.FillTexts(texts);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
