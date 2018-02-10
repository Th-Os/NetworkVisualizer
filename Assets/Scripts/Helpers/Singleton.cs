namespace Helpers
{

    public class Singleton<T> where T : class, new()
    {

        private static T mInstance;

        public static T Instance
        {
            get
            {
                return mInstance ?? (mInstance = new T());
            }
        }
    }
}
