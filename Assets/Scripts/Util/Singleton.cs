using UnityEngine;

namespace PickAR.Util {

    /// <summary>
    /// Singleton behaviour class, used for components that should only have one instance.
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T> {
        /// <summary> The singleton instance of the class. </summary>
        private static T instance;
        /// <summary> The singleton instance of the class. </summary>
        public static T Instance {
            get { return instance; }
        }

        /// <summary>
        /// Base awake method that sets the singleton's unique instance.
        /// </summary>
        protected virtual void Awake() {
            if (instance != null) {
                Debug.LogErrorFormat("Trying to instantiate a second instance of singleton class {0}", GetType().Name);
            } else {
                instance = (T) this;
            }
        }
    }
}