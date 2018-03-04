using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace NetworkVisualizer
{

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
           //_line.material.mainTextureScale = new Vector2(Vector2.Distance(_line.GetPosition(0), _line.GetPosition(1)), 1f);
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