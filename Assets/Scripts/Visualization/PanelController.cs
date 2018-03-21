using UnityEngine;
using UnityEngine.UI;
using NetworkVisualizer.Enums;

namespace NetworkVisualizer.Visual
{
     /// <summary>
     /// PanelController is used to create, position and fill a panel (device or connection details).
     /// </summary>
    public class PanelController : MonoBehaviour
    {

        public GameObject ConnectionPanel;
        public GameObject DevicePanel;
        public float PanelOffsetY = 2f;
        public bool HasPanel { get; private set; }

        private GameObject _panelParent;

        private DeviceConnection _deviceConnection;
        private GameObject _device;
        private PanelType _type;
        private NetworkObject _networkObject;

        /// <summary>
        /// Initializes a specific panel, which is specified by the <see cref="PanelType"/>, and adds data in form of a <see cref="NetworkObject"/> to it.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        public void InitPanel(PanelType type, NetworkObject obj)
        { 
            _type = type;
            _networkObject = obj;
            //http://wiki.unity3d.com/index.php?title=CameraFacingBillboard
            transform.LookAt(_panelParent.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
            GameObject panel = null;
            switch (_type)
            {
                case PanelType.Connection:
                    panel = Instantiate(ConnectionPanel, transform);
                    break;
                case PanelType.Device:
                    panel = Instantiate(DevicePanel, transform);
                    break;
            }
            if (panel != null)
            {
                InitPanel(panel);
            }


            HasPanel = true;
        }

        public void SetDataLayer(object sender)
        {
            if(sender.GetType() == typeof(GameObject))
            {
                _device = sender as GameObject;
            }
            if(sender.GetType() == typeof(DeviceConnection))
            {
                _deviceConnection = sender as DeviceConnection;
            }
        }

        private void InitPanel(GameObject panel)
        {
            panel.GetComponent<RectTransform>().localPosition = new Vector3(0f, PanelOffsetY, 0f);
            panel.GetComponent<RectTransform>().localRotation.SetLookRotation(Camera.main.transform.position);
            panel.GetComponentInChildren<Canvas>().worldCamera = Camera.main;

            FillPanel(panel, _networkObject);
        }

        private void FillPanel(GameObject panel, NetworkObject obj)
        {
            if (obj != null)
                obj.FillTexts(panel.GetComponentsInChildren<Text>());
        }

        private void Start()
        {
            HasPanel = false;
        }
    }
}