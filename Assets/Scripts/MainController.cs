using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NetworkVisualizer.Objects;

namespace NetworkVisualizer
{

    public class MainController : MonoBehaviour
    {

        public string m_Uri;

        private bool trackGaze;

        // Use this for initialization
        void Start()
        {
            // MqttController.Start(m_Uri);
            Events.OnTestStarted += OnTestStarted;
            Events.OnTestEnded += OnTestEnded;

            trackGaze = false;
;
        }

        // Update is called once per frame
        void Update()
        {
            if(trackGaze)
            {
                RaycastHit hitInfo;
                if (Physics.Raycast(
                        Camera.main.transform.position,
                        Camera.main.transform.forward,
                        out hitInfo,
                        20.0f,
                        Physics.DefaultRaycastLayers))
                {
                    // If the Raycast has succeeded and hit a hologram
                    // hitInfo's point represents the position being gazed at
                    // hitInfo's collider GameObject represents the hologram being gazed at
                    GameObject hitObject = hitInfo.collider.gameObject;
                    if (hitObject != null)
                    {
                        DisplayDataOf(hitObject);

                    }
                }
            }

        }

        void DisplayDataOf(GameObject obj)
        {
            Debug.Log("Trying to display data of " + obj.name + " with id " + obj.GetInstanceID());
        }

        void OnTestStarted(int test)
        {
            trackGaze = true;
        }

        void OnTestEnded()
        {
            trackGaze = false;
        }
    }
}