using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using NetworkVisualizer.Enums;

namespace NetworkVisualizer
{

    public class Interaction : MonoBehaviour, IInteractable
    {

        [HideInInspector]
        public GameObject CurrentFocus;

        [HideInInspector]
        public bool OnFocus;

        public virtual void OnFocusEnter()
        {
            OnFocus = true;
            CurrentFocus = gameObject;
            EventHandler.Broadcast(Events.FOCUS, CurrentFocus);
        }

        public virtual void OnFocusExit()
        {
            OnFocus = false;
            CurrentFocus = null;
            EventHandler.Broadcast(Events.UNFOCUS, CurrentFocus);
        }

        public virtual void OnClick()
        {
            Debug.Log("Clicked on: " + gameObject.name);
        }
    }
}