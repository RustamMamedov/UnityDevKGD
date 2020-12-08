using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    public class AutoDodge : MonoBehaviour {

        [SerializeField]
        private BoxCollider _detectCollider;

        [ValidateInput(nameof(ValidateDistance), "Dostance should be positive")]
        [SerializeField]
        private float _distanceForDodge;

        [SerializeField]
        private GameObject _playerCar;

        private PlayerCar _carScript;

        private void Start() {
            _playerCar.TryGetComponent<PlayerCar>(out _carScript);
        }

        private void OnEnable() {
            _detectCollider.center = new Vector3(0, 0, _distanceForDodge / 2);
            _detectCollider.size = new Vector3(0, 0, _distanceForDodge);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("EnemyCar")) {
                int currentRoad = _carScript.GetCurrentRoad();
                bool isAutoDodge = Random.Range(0, 4) == 3 ? true : false;
                int randomRoad = Random.Range(0, 2) == 1 ? 1 : -1;
                if (isAutoDodge) {
                    switch (currentRoad) {
                        case -1:
                            _carScript.MoveToRoad(0);
                            break;
                        case 0:
                            _carScript.MoveToRoad(randomRoad);
                            break;
                        case 1:
                            _carScript.MoveToRoad(0);
                            break;
                    }
                } else {
                    Debug.Log("Pass");
                }
            }
        }

        private bool ValidateDistance(float distance) {
            return distance > 0;
        }
    }
}
