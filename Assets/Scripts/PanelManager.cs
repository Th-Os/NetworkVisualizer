using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NetworkVisualizer.Enums;

namespace NetworkVisualizer {
    public class PanelManager : MonoBehaviour {

        public GameObject Panel;

        private GameObject _panels;
        private GameObject _currentPanel;

        // Use this for initialization
        void Start() {
            _panels = gameObject;
        }

        public void ShowPanel(object sender, Vector3 position, PanelType type, NetworkObject networkObject)
        {
            if (_currentPanel != null && _currentPanel.activeInHierarchy)
                Destroy(_currentPanel);

            _currentPanel = CreatePanel(position);
            _currentPanel.GetComponent<PanelController>().Init(type, networkObject);
            _currentPanel.GetComponent<PanelController>().SetDataLayer(sender);

        }

        private GameObject CreatePanel(Vector3 position)
        {
            GameObject parent = Instantiate(Panel, _panels.transform);
            parent.transform.position = position;
            return parent;
        }
    }
}