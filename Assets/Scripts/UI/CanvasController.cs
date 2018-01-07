using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

    public GameObject TestUI;
    public GameObject DefineUI;
    public GameObject WorldUI;

	// Use this for initialization
	void Start () {        
        Events.OnTestUISwitched += ToggleTestUI;

        DefineUI = Instantiate(DefineUI, transform);
        TestUI = Instantiate(TestUI, transform);
        WorldUI = Instantiate(WorldUI, transform);

        TestUI.SetActive(false);
        WorldUI.SetActive(false);
    }

    void OnDeviceFound(GameObject obj)
    {
        DefineUI.GetComponentInChildren<Text>().text = "Device " + obj.name + " defined with position " + obj.transform.position;
    }
	
    void ToggleTestUI()
    {

        if(DefineUI.activeInHierarchy)
        {
            DefineUI.SetActive(false);            
        }

        bool active = false;
        if(TestUI.activeInHierarchy)
        {
            active = true;
        }
        
        TestUI.SetActive(!active);
        WorldUI.SetActive(active);

        Debug.Log("Toggle TEST UI");
    }
}
