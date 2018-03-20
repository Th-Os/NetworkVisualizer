using UnityEngine;

namespace Helpers
{
    /// <summary>
    /// Contains useful methods for different occasions.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Searches upwards for a type in the different layers such as parent or self.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
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
