using UnityEngine;

namespace Utilities {
    
    public class GameSingletonBase<TSelf> : MonoBehaviour
        where TSelf : GameSingletonBase<TSelf> {

        // Fields.

        private static TSelf _instance;


        // Properties.

        public static TSelf Instance => _instance;


        // Life cycle.

        protected virtual void Awake() {
            if (_instance != null) {
                Destroy(gameObject);
                return;
            }
            _instance = (TSelf) this;
            DontDestroyOnLoad(gameObject);
        }


    }

}
