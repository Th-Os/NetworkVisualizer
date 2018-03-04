using UnityEngine;

namespace NetworkVisualizer
{

    public class UIView : MonoBehaviour, IUIInteractable
    {
        private CanvasGroup _canvasGroup;

        public void Hide()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0;
        }

        public void Show()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.alpha = 1;
        }

        // Use this for initialization
        void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}