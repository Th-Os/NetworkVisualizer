using UnityEngine;

namespace NetworkVisualizer.Visual
{
    /// <summary>
    /// A UIView is responsible for showing and hiding an UI element.
    /// </summary>
    public class UIView : MonoBehaviour, IUIInteractable
    {
        private CanvasGroup _canvasGroup;

        /// <summary>
        /// Hide <see cref="CanvasGroup"/>.
        /// </summary>
        public void Hide()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.alpha = 0;
        }

        /// <summary>
        /// Show <see cref="CanvasGroup"/>.
        /// </summary>
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