using System;

namespace Helpers
{

    public class Singleton<T> where T : class
    {
        /// <summary>
        /// Static instance. Needs to use lambda expression
        /// to construct an instance (since constructor is private).
        /// </summary>
        private static readonly Lazy<T> mInstance = new Lazy<T>(() => CreateInstanceOfT());

        public static T Instance
        {
            get
            {
                return mInstance.Value;
            }
        }

        /// <summary>
        /// Creates an instance of T via reflection since T's constructor is expected to be private.
        /// </summary>
        /// <returns></returns>
        private static T CreateInstanceOfT()
        {
            return Activator.CreateInstance(typeof(T), true) as T;
        }
    }
}
