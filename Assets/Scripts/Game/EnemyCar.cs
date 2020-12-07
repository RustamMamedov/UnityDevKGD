using Events;
using UnityEngine;

namespace Game {

    public class EnemyCar : Car {

        [SerializeField]
        private EventDispatcher _carCollisionEventDispatcher;

        [SerializeField]
        private EventDispatcher _carDodgedEventDispatcher;

        [SerializeField]
        private ScriptableIntValue _dodgedScore;

        [SerializeField]
        private ScriptableStringValue _dodgedCarName;

        [SerializeField]
        private GameObject _star;

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) {
                _carCollisionEventDispatcher.Dispatch();
            }
        }

        private void OnEnable() {
            var zPlusStarPosition = (Random.Range(0, 2) ==0 ? -1f : 1f)*_carSettings.distanceToStar;
            Instantiate(_star, new Vector3(gameObject.transform.position.x,0,gameObject.transform.position.z+zPlusStarPosition),Quaternion.identity); 
        }

        private void OnTriggerExit(Collider other) {
            if (other.CompareTag("PlayerDodge")) {
                _dodgedScore.value = _carSettings.dodgeScore;
                _dodgedCarName.value = _carSettings.name;
                _carDodgedEventDispatcher.Dispatch();
            }
        }
    }
}