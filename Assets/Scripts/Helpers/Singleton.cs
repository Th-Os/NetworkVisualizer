using System;

namespace Helpers
{
    /// <summary>
    /// Generic singleton pattern.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class, new()
    {
        private static T mInstance;

        //??  = if not null do 
        public static T Instance
        {
            get
            {
                return mInstance ?? (mInstance = new T());
            }
        }
        /*
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
        */
    }
}
