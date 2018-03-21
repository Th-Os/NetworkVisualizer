using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkVisualizer.Enums;

namespace NetworkVisualizer.Visual
{
    /// <summary>
    /// The PanalManager manages the outline for the detail panels.
    /// </summary>
    public class PanelManager : MonoBehaviour
    {

        public GameObject Panel;

        private GameObject _panels;
        private GameObject _currentPanel;

        // Use this for initialization
        void Start() {
            _panels = gameObject;
        }

        /// <summary>
        /// After the panel created the outline for a detail view, 
        /// it lets the <see cref="PanelController"/> do the creation of the actual view and the filling of the data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="position"></param>
        /// <param name="type"></param>
        /// <param name="networkObject"></param>
        public void ShowPanel(object sender, Vector3 position, PanelType type, NetworkObject networkObject)
        {
            if (_currentPanel != null && _currentPanel.activeInHierarchy)
                Destroy(_currentPanel);

            _currentPanel = CreateOutline(position);
            _currentPanel.GetComponent<PanelController>().InitPanel(type, networkObject);
            _currentPanel.GetComponent<PanelController>().SetDataLayer(sender);

        }

        private GameObject CreateOutline(Vector3 position)
        {
            GameObject parent = Instantiate(Panel, _panels.transform);
            parent.transform.position = position;
            return parent;
        }
    }
}