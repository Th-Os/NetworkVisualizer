using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

namespace NetworkVisualizer
{
    public class FocusBehaviour : Interaction
    {
        /*
        public Color OnFocusColor;
        public Sprite OnFocusSprite;

        private Image _image;
        private Color _color;
        private Sprite _oldSprite;

        private Selectable _selectable;
        */

        public void Start()
        {
            /*
            _image = GetComponent<Image>();
            _selectable = GetComponent<Selectable>();
            _color = _image.color;
            _oldSprite = _image.sprite;
            */
        }

        public override void OnFocusEnter()
        {
            base.OnFocusEnter();
            //if (OnFocusColor != null)
            //    _image.color = OnFocusColor;
            /*
            if(_selectable != null)
                _selectable.Select();

            _image.sprite = OnFocusSprite;
            */
        }

        public override void OnFocusExit()
        {
            base.OnFocusExit();
            
        }


    }
}