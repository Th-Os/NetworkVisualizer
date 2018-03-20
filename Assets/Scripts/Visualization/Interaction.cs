using UnityEngine;
using NetworkVisualizer.Enums;

namespace NetworkVisualizer.Visual
{
    /// <summary>
    /// Basic Interaction class.
    /// </summary>
    public class Interaction : MonoBehaviour, IInteractable
    {

        [HideInInspector]
        public GameObject CurrentFocus;

        [HideInInspector]
        public bool OnFocus;

        /// <summary>
        /// Called, when the user looks at this object.
        /// </summary>
        public virtual void OnFocusEnter()
        {
            OnFocus = true;
            CurrentFocus = gameObject;
            EventHandler.Broadcast(Events.FOCUS, CurrentFocus);
        }

        /// <summary>
        /// Called, when the users gaze moves elsewhere after being on the object.
        /// </summary>
        public virtual void OnFocusExit()
        {
            OnFocus = false;
            CurrentFocus = null;
            EventHandler.Broadcast(Events.UNFOCUS, CurrentFocus);
        }

        /// <summary>
        /// Called, when the user focuses on the object and execute a tap.
        /// </summary>
        public virtual void OnClick()
        {
            Debug.Log("Clicked on: " + gameObject.name);
        }
    }
}