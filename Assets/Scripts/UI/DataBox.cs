using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NetworkVisualizer
{

    public class DataBox : Interaction
    {
        public Sprite HighlightSprite;

        private Sprite _standardSprite;
        private Image _image;

        // Use this for initialization
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