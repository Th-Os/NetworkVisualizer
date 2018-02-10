using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helpers;

public class CanvasController : MonoBehaviour {

    public GameObject TestUI;
    public GameObject DefineUI;
    public GameObject WorldUI;

	// Use this for initialization
	void Start () {        
        Events.OnTestStarted += OnWorldUI;
        Events.OnTestEnded += OnTestUI;
        Events.OnDefineProcessStarted += OnDefineUI;
        Events.OnDefineProcessEnded += OnTestUI;


        DefineUI = Instantiate(DefineUI, transform);
        TestUI = Instantiate(TestUI, transform);
        WorldUI = Instantiate(WorldUI, transform);

        TestUI.SetActive(false);
        WorldUI.SetActive(false);
    }

    //When the change to singleton is needed.
    void Init(GameObject TestUI, GameObject DefineUI, GameObject WorldUI)
    {
        this.DefineUI = Instantiate(DefineUI, transform);
        this.TestUI = Instantiate(TestUI, transform);
        this.WorldUI = Instantiate(WorldUI, transform);

        Events.OnTestStarted += OnWorldUI;
        Events.OnTestEnded += OnTestUI;
        Events.OnDefineProcessStarted += OnDefineUI;
        Events.OnDefineProcessEnded += OnTestUI;

        TestUI.SetActive(false);
        WorldUI.SetActive(false);
    }

    void OnDefineUI()
    {
        if (!DefineUI.activeInHierarchy)
            DefineUI.SetActive(true);

        Events.OnDeviceFound += OnDeviceFound;
    }

    void OnTestUI()
    {
        Events.OnDeviceFound -= OnDeviceFound;
        if (DefineUI.activeInHierarchy)
            DefineUI.SetActive(false);


        if (WorldUI.activeInHierarchy)
            WorldUI.SetActive(false);

        TestUI.SetActive(true);
        
    }

    void OnWorldUI(int test)
    {
        if (TestUI.activeInHierarchy)
            TestUI.SetActive(false);

        WorldUI.SetActive(true);
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


    void OnDeviceFound(Transform obj)
    {
        DefineUI.GetComponentInChildren<Text>().text = "Device " + obj.name + " defined with position " + obj.transform.position;
    }

}
