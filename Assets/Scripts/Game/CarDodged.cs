using UnityEngine;
using Events;

namespace Game{
    public class CarDodged : MonoBehaviour{
        [SerializeField]
        private EventDispatcher _enemyCarsBackTriggerEventDispatch;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("CarBack")) {
                _enemyCarsBackTriggerEventDispatch.Dispatch();
                Debug.Log("erer");
            }
        }
    }
}