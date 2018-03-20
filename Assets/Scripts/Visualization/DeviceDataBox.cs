using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NetworkVisualizer.Visual
{
    /// <summary>
    /// Functionality of a <see cref="DataBox"/> specific to a device.
    /// </summary>
    public class DeviceDataBox : DataBox
    {
        override protected void Start()
        {
            base.Start();
        }

        public override void OnFocusEnter()
        {
            base.OnFocusEnter();

        }

        public override void OnFocusExit()
        {
            base.OnFocusExit();
        }

        public override void OnClick()
        {
            base.OnClick();
        }
    }
}