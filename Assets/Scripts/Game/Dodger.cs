using UnityEngine;
using Game;
using Events;

namespace Game {

    public class Dodger : MonoBehaviour {

        [SerializeField]
        private EventDispatcher _dodgeDispatcher;

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.GetComponent<EnemyCar>() != null) {
                _dodgeDispatcher.Dispatch();
            }
        }
    }
}
