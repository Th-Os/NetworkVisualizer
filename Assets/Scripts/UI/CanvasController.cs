using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helpers;
using NetworkVisualizer.Enums;
using System;

namespace NetworkVisualizer { 

//important: https://forum.unity.com/threads/unity-ui-on-the-hololens.394629/
    public class CanvasController : Singleton<CanvasController> {

        public GameObject Parent;
        public GameObject TestUI;
        public GameObject DefineUI;
        public GameObject VisualizeUI;
        public GameObject MenuUI;

        private Vector3 _panelOffset;

        private Transform _testInteraction;
        private Transform _testPanels;
        private bool _testShown;
        private bool _menuShown;

        private GameObject _currentUI;


        //When the change to singleton is needed.
        public void Init(GameObject Parent, GameObject TestUI, GameObject DefineUI, GameObject VisualizeUI, GameObject MenuUI)
        {
            this.Parent = Parent;
            this.DefineUI = DefineUI;
            this.TestUI = TestUI;
            this.VisualizeUI = VisualizeUI;
            this.MenuUI = MenuUI;

            //not needed right now
            //EventHandler.OnTestStarted += OnVisualizeUI;
            EventHandler.OnShowTest += OnTestUI;
            EventHandler.OnDefineProcessStarted += OnDefineUI;
            EventHandler.OnShowMenu += OnMenuUI;
            EventHandler.OnHideMenu += OnMenuUI;

            _panelOffset = new Vector3(0f, 4f, 0f);
        }

        private void OnMenuUI(States state)
        {
            DestroyUI();
            //funktioniert besser mit lookrotation
            _currentUI = GameObject.Instantiate(MenuUI, Camera.main.transform.forward, Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up), Parent.transform);
            _menuShown = !_menuShown;
            _currentUI.SetActive(_menuShown);
            if(_menuShown)
            {
                if(_currentUI.GetComponent<Canvas>().worldCamera == null)
                    _currentUI.GetComponent<Canvas>().worldCamera = Camera.main;
            }
        }

        void OnDefineUI()
        {
            DestroyUI();
            _currentUI = GameObject.Instantiate(DefineUI, Parent.transform);
            Debug.Log("Start DefineUI");
            if (!_currentUI.activeInHierarchy)
                _currentUI.SetActive(true);
            
            EventHandler.OnDeviceDefined += OnDeviceFound;
            _currentUI.GetComponent<Canvas>().worldCamera = Camera.main;
            _currentUI.GetComponentInChildren<Text>().text = "Device Define Process started.";
        }

        void OnTestUI(int test)
        {
            DestroyUI();
            Debug.Log("Start TestUI");
            EventHandler.OnDeviceDefined -= OnDeviceFound;

            _currentUI = GameObject.Instantiate(TestUI, Camera.main.transform.forward, Camera.main.transform.rotation, Parent.transform);

            _currentUI.SetActive(true);
            _currentUI.GetComponent<Canvas>().worldCamera = Camera.main;
            _testInteraction = _currentUI.transform.Find("Interaction");
            _testPanels = _currentUI.transform.Find("Tests");
            _testShown = true;
        }

        void OnVisualizeUI(int test)
        {
            Debug.Log("Start VisualizeUI");
            DestroyUI();
            _currentUI = GameObject.Instantiate(VisualizeUI, Parent.transform);
            _currentUI.SetActive(true);
            _currentUI.GetComponent<Canvas>().worldCamera = Camera.main;
        }

        void OnDeviceFound(Transform obj)
        {
            _currentUI.GetComponentInChildren<Text>().text = "Device " + obj.name + " defined with position " + obj.transform.position;
        }

        void OnClick(Transform obj)
        {
            if (obj != null)
            {
                Debug.Log("Canvas Clicked: " + obj.name);
            }
        }

        void DestroyUI()
        {
            if (_currentUI != null)
                GameObject.Destroy(_currentUI);
            _currentUI = null;
        }
    }
}