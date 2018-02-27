using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class Interaction : MonoBehaviour, IInteractable {

    [HideInInspector]
    public GameObject CurrentFocus;

    [HideInInspector]
    public bool OnFocus;

    public virtual void OnFocusEnter()
    {
        OnFocus = true;
        CurrentFocus = gameObject;
        Events.Broadcast(Events.EVENTS.FOCUS, CurrentFocus);
    }

    public virtual void OnFocusExit()
    {
        OnFocus = false;
        CurrentFocus = null;
        Events.Broadcast(Events.EVENTS.UNFOCUS, CurrentFocus);
    }

    public virtual void OnClick()
    {
        Debug.Log("Clicked on: " + gameObject.name);
    }
}
