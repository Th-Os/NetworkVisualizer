using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{

    public string m_Uri;

    // Use this for initialization
    void Start()
    {
        EventManager.AddHandler(EVNT.StartTest, BeginTest);
        MqttController.Start(m_Uri);
    }

    // Update is called once per frame
    void Update()
    {
        //not working for hololens ...
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("key pressed");
            //EventManager.Broadcast(EVNT.NewConnection, "hello");
        }
    }

    private void BeginTest(string name)
    {
        EventManager.Broadcast(EVNT.SwitchTestUI, "");
        Debug.Log("Beginning " + name);
    }
}
