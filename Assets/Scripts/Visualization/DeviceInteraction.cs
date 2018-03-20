using UnityEngine;

namespace NetworkVisualizer.Visual
{
    /// <summary>
    /// DeviceInteraction handles the interactions by changing the color of the device shell.
    /// </summary>
    class DeviceInteraction : Interaction
    {
        /// <summary>
        /// When the device has the focus of the user it will change the outer material to this one.
        /// </summary>
        public Material DeviceHighlightMaterial;

        private Material _standardDeviceMaterial;
        private Renderer _shellRenderer;

        private void Awake()
        {
            _shellRenderer = transform.Find("Shell").gameObject.GetComponent<Renderer>();
        }

        public override void OnFocusEnter()
        {
            base.OnFocusEnter();
            Debug.Log("Focus " + gameObject.name);
            if (_shellRenderer != null && DeviceHighlightMaterial != null)
            {
                if (_standardDeviceMaterial == null)
                    _standardDeviceMaterial = _shellRenderer.material;

                _shellRenderer.material = DeviceHighlightMaterial;
            }
        }

        public override void OnFocusExit()
        {
            base.OnFocusExit();
            Debug.Log("Unfocus " + gameObject.name);
            if (_shellRenderer != null)
            {
                _shellRenderer.material = _standardDeviceMaterial;
            }
        }
    }
}
