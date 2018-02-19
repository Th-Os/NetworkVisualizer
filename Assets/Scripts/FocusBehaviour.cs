using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

public class FocusBehaviour : MonoBehaviour, IFocusable {

    public Color OnFocus;

    private Image _image;
    private Color _color;

    public void Start()
    {
        _image = GetComponent<Image>();
        _color = _image.color;
    }

    public void OnFocusEnter()
    {
        if (OnFocus != null)
            _image.color = OnFocus;
    }

    public void OnFocusExit()
    {
        _image.color = _color;
    }


}
