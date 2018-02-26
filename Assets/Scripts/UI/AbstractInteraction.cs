using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class AbstractInteraction : MonoBehaviour, IInteractable {

    [HideInInspector]
    public GameObject CurrentFocus;

    [HideInInspector]
    public bool OnFocus;

    public virtual void OnFocusEnter()
    {
        OnFocus = true;
        CurrentFocus = gameObject;
    }

    public virtual void OnFocusExit()
    {
        OnFocus = false;
        CurrentFocus = null;
    }

    public virtual void OnClick()
    {

    }

    public virtual void OnInputClicked(InputClickedEventData eventData)
    {
    }

}
