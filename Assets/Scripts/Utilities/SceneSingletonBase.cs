using UnityEngine;

namespace Utilities {
    
    public class SceneSingletonBase<TSelf> : MonoBehaviour
        where TSelf : SceneSingletonBase<TSelf> {

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
        }

        protected virtual void OnDestroy() {
            if (_instance == this) {
                _instance = null;
            }
        }


    }

}
