using UnityEngine;

namespace Helpers
{
    public static class Utils
    {
        public static T SearchFor<T>(GameObject obj) where T : MonoBehaviour
        {
            while (obj.GetComponent<T>() == null)
            {
                Transform parent =  obj.transform.parent;
                if(parent == null)
                    return null;

                obj = parent.gameObject;
            }

            return obj.GetComponent<T>();
        }
    }
}
