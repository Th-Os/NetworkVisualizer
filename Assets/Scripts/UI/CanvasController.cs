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
    public GameObject VisualizeUI;

    private Vector3 _panelOffset;

    private Transform _testInteraction;
    private Transform _testPanels;
    private bool _testShown;


    //When the change to singleton is needed.
    public void Init(GameObject Parent, GameObject TestUI, GameObject DefineUI, GameObject VisualizeUI)
    {
        this.Parent = Parent;
        this.DefineUI = GameObject.Instantiate(DefineUI, Parent.transform);
        this.TestUI = GameObject.Instantiate(TestUI, Parent.transform);
        this.VisualizeUI = GameObject.Instantiate(VisualizeUI, Parent.transform);

        Events.OnTestStarted += OnWorldUI;
        Events.OnTestEnded += OnTestUI;
        Events.OnDefineProcessStarted += OnDefineUI;
        Events.OnDefineProcessEnded += OnTestUI;

        DefineUI.SetActive(false);
        TestUI.SetActive(false);
        VisualizeUI.SetActive(false);

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


        if (VisualizeUI.activeInHierarchy)
            VisualizeUI.SetActive(false);

        TestUI.SetActive(true);
        TestUI.GetComponent<Canvas>().worldCamera = Camera.main;
        _testInteraction = TestUI.transform.Find("Interaction");
        _testPanels = TestUI.transform.Find("Tests");
        _testShown = true;
    }

    void OnWorldUI(int test)
    {
        Debug.Log("Start WorldUI");
        if (TestUI.activeInHierarchy)
            TestUI.SetActive(false);

        VisualizeUI.SetActive(true);
        VisualizeUI.GetComponent<Canvas>().worldCamera = Camera.main;
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
}
