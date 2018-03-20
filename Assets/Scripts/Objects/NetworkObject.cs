using UnityEngine.UI;

public abstract class NetworkObject {

    /// <summary>
    /// Fills the given <see cref="Text[]"/> with the fields of a network data object.
    /// </summary>
    /// <param name="texts"></param>
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
