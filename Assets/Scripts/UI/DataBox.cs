using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class DataBox : Interaction {

    public Transform CancelPanel;

    private Canvas _canvas;
    private Transform _cancelPanel;

    private bool isOnCancel;

    // Use this for initialization
    protected virtual void Start()
    {
        _canvas = GetComponentInChildren<Canvas>();
    }

    public override void OnFocusEnter()
    {
        base.OnFocusEnter();
        Debug.Log("DataBox: Enter " + gameObject.name);
    }

    public override void OnFocusExit()
    {
        Debug.Log("DataBox: Exit " + gameObject.name);
        base.OnFocusExit();
        if (isOnCancel)
        {
            isOnCancel = false;
            Destroy(_cancelPanel);
        }
    }

    public override void OnClick()
    {
        base.OnClick();
        Debug.Log("DataBox: Clicked virtual" + gameObject.name);
        if (OnFocus)
        {
            if (isOnCancel)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
            else
            {
                isOnCancel = true;
                _cancelPanel = Instantiate(CancelPanel, _canvas.transform);
            }

        }
    }
}
