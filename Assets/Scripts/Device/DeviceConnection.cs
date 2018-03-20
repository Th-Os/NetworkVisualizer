using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace NetworkVisualizer.Visual
{
    /// <summary>
    /// A DeviceConnection defines and saves the two related nodes. Furthermore it changes the material of a device on focus.
    /// </summary>
    public class DeviceConnection : Interaction
    {

        public Transform Source { get; set; }
        public Transform Target { get; set; }

        public Material HighlightMaterial;

        private LineRenderer _line;
        private Material _oldMaterial;

        public void Start()
        {
            _line = GetComponent<LineRenderer>();
            _oldMaterial = _line.material;
        }

        public override void OnFocusEnter()
        {
            base.OnFocusEnter();
            Debug.Log("DeviceConnection " + Source.name + " and " + Target.name + " has Focus");
            if (_line != null && HighlightMaterial != null)
            {
                _line.material = HighlightMaterial;
            }
        }

        public override void OnFocusExit()
        {
            base.OnFocusExit();
            Debug.Log("DeviceConnection " + Source.name + " and " + Target.name + " has no Focus anymore");
            if (_line != null)
            {
                _line.material = _oldMaterial;
            }
        }

    }
}