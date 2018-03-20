using UnityEngine;
using UnityEngine.UI;

namespace NetworkVisualizer.Visual
{
    /// <summary>
    /// The Databox represents a detail panel in the application.
    /// It implements specific interactions and destroys the panel on a click.
    /// </summary>
    public class DataBox : Interaction
    {
        /// <summary>
        /// A sprite that will be used, when the box has the focus of the user.
        /// </summary>
        public Sprite HighlightSprite;

        private Sprite _standardSprite;
        private Image _image;

        protected virtual void Start()
        {
            _image = GetComponentInChildren<Image>();
            _standardSprite = _image.sprite;
        }

        public override void OnFocusEnter()
        {
            base.OnFocusEnter();
            Debug.Log("DataBox: Enter " + gameObject.name);
            _image.sprite = HighlightSprite;
        }

        public override void OnFocusExit()
        {
            base.OnFocusExit();
            Debug.Log("DataBox: Exit " + gameObject.name);            
            _image.sprite = _standardSprite;
        }

        public override void OnClick()
        {
            base.OnClick();
            Debug.Log("DataBox: Clicked virtual" + gameObject.name);
            if (OnFocus)
            {
                Destroy(gameObject.transform.parent.gameObject);
                /*
                if (isOnCancel)
                {
                    
                }
                else
                {
                    isOnCancel = true;
                    _cancelPanel = Instantiate(CancelPanel, _canvas.transform);
                }
                */
            }
        }
    }
}