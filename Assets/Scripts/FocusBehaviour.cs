using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

public class FocusBehaviour : UIInteraction {

    public Color OnFocusColor;

    private Image _image;
    private Color _color;

    public void Start()
    {
        _image = GetComponent<Image>();
        _color = _image.color;
    }

    public override void OnFocusEnter()
    {
        base.OnFocusEnter();
        if (OnFocusColor != null)
            _image.color = OnFocusColor;
        Events.Broadcast(Events.EVENTS.FOCUS_TEST, transform);
    }

    public override void OnFocusExit()
    {
        base.OnFocusExit();
        _image.color = _color;
        Events.Broadcast(Events.EVENTS.UNFOCUS_TEST, transform);
    }


}
