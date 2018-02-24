using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helpers;

//important: https://forum.unity.com/threads/unity-ui-on-the-hololens.394629/
public class CanvasController : Singleton<CanvasController> {

    public GameObject Parent;
    public GameObject TestUI;
    public GameObject DefineUI;
    public GameObject WorldUI;

    private Vector3 _panelOffset;

    private Transform _testInteraction;
    private Transform _testPanels;
    private bool _testShown;

	// Use this for initialization
    /*
	void Start () {        
        Events.OnTestStarted += OnWorldUI;
        Events.OnTestEnded += OnTestUI;
        Events.OnDefineProcessStarted += OnDefineUI;
        Events.OnDefineProcessEnded += OnTestUI;


        DefineUI = Instantiate(DefineUI, transform);
        TestUI = Instantiate(TestUI, transform);
        WorldUI = Instantiate(WorldUI, transform);

        DefineUI.SetActive(false);
        TestUI.SetActive(false);
        WorldUI.SetActive(false);

        _panelOffset = new Vector3(0f, 4f, 0f);
    }
    */

    //When the change to singleton is needed.
    public void Init(GameObject Parent, GameObject TestUI, GameObject DefineUI, GameObject WorldUI)
    {
        this.Parent = Parent;
        this.DefineUI = GameObject.Instantiate(DefineUI, Parent.transform);
        this.TestUI = GameObject.Instantiate(TestUI, Parent.transform);
        this.WorldUI = GameObject.Instantiate(WorldUI, Parent.transform);

        Events.OnTestStarted += OnWorldUI;
        Events.OnTestEnded += OnTestUI;
        Events.OnDefineProcessStarted += OnDefineUI;
        Events.OnDefineProcessEnded += OnTestUI;

        DefineUI.SetActive(false);
        TestUI.SetActive(false);
        WorldUI.SetActive(false);

        _panelOffset = new Vector3(0f, 4f, 0f);
    }

    void OnDefineUI()
    {
        Debug.Log("Start DefineUI");
        if (!DefineUI.activeInHierarchy)
            DefineUI.SetActive(true);

        Events.OnDeviceDefined += OnDeviceFound;
        DefineUI.GetComponent<Canvas>().worldCamera = Camera.main;
        DefineUI.GetComponentInChildren<Text>().text = "Device Define Process started.";
    }

    void OnTestUI()
    {
        Debug.Log("Start TestUI");
        Events.OnDeviceDefined -= OnDeviceFound;
        if (DefineUI.activeInHierarchy)
            DefineUI.SetActive(false);


        if (WorldUI.activeInHierarchy)
            WorldUI.SetActive(false);

        TestUI.SetActive(true);
        TestUI.GetComponent<Canvas>().worldCamera = Camera.main;
        _testInteraction = TestUI.transform.Find("Interaction");
        _testPanels = TestUI.transform.Find("Tests");
        _testShown = true;
        Events.OnHighlight += OnHighlight;
        Events.OnHide += OnHide;
    }

    void OnWorldUI(int test)
    {
        Debug.Log("Start WorldUI");
        if (TestUI.activeInHierarchy)
            TestUI.SetActive(false);

        WorldUI.SetActive(true);
        WorldUI.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    void OnDeviceFound(Transform obj)
    {
        DefineUI.GetComponentInChildren<Text>().text = "Device " + obj.name + " defined with position " + obj.transform.position;
    }

    void OnClick(Transform obj)
    {
        if (obj != null)
        {
            Debug.Log("Clicked: " + obj.name);
        }
    }

    void OnHighlight(Transform obj)
    {
        if (obj != null)
        {
            //TODO Highlight Panel with hMaterial or color

            Debug.Log(Parent.name + ": Highlight " + obj.name);
        }
    }

    void OnHide(Transform obj)
    {
        if (obj != null)
        {
            //TODO Hide Panel hMaterial

            Debug.Log(Parent.name + ": Hide " + obj.name);
        }
    }

    void OnTestToggleHide()
    {
        if(TestUI.activeInHierarchy && _testInteraction != null && _testPanels != null)
        {
            Text text = _testInteraction.GetComponentInChildren<Text>();
            _testPanels.gameObject.SetActive(!_testShown);
            if (_testShown)
            {
                _testShown = false;
                text.text = "Show";
            }else
            {
                _testShown = true;
                text.text = "Hide";
            }
            
        }

    }

    //Really there? Test second approach (VisualizationManager).
    //This is a fallback solution.
    /*
    GameObject DrawConnectionPanel(Transform source, Transform target)
    {
        if(WorldUI.activeInHierarchy)
        {
            Vector3 position = Vector3.Lerp(source.position, target.position + _panelOffset, 0.5f);
            GameObject connectionPanel = Instantiate(ConnectionUIPanel, position, WorldUI.transform.rotation, WorldUI.transform);

            //Fill with data.

            return connectionPanel;
        }
        return null;
    }

    GameObject DrawDevicePanel(Transform device)
    {
        if (WorldUI.activeInHierarchy)
        {
            Vector3 position = device.position + _panelOffset;
            GameObject devicePanel = Instantiate(DeviceUIPanel, position, WorldUI.transform.rotation, WorldUI.transform);


            return devicePanel;
        }
        return null;
    }
	
    //deprecated code
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
    */

}
