using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public abstract class NetworkObject {

    public abstract void FillTexts(Text[] texts);

    protected string DeviceNameToUpperCase(string name)
    {
        if (name.Contains("_"))
        {
            string[] array = name.Split('_');
            return array[0].ToUpper() + array[1];
        }
        return name.ToUpper();
    }

}
