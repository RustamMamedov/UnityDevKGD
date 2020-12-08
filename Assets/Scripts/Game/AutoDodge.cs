using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class AutoDodge : MonoBehaviour {

        [SerializeField]
        private BoxCollider _detectCollider;

        [SerializeField]
        private float _distanceForDodge;

        [SerializeField]
        private GameObject _playerCar;

        private PlayerCar _carScript;

        private void OnEnable() {
            _playerCar.TryGetComponent<PlayerCar>(out _carScript);
            _detectCollider.center = new Vector3(0, 0, _distanceForDodge / 2);
            _detectCollider.size = new Vector3(0, 0, _distanceForDodge);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("EnemyCar")) {
                int currentRoad = _carScript.GetCurrentRoad();
                switch (currentRoad) {
                    case -1:
                        _carScript.MoveToRoad(0);
                        break;
                    case 0:
                        _carScript.MoveToRoad(1);
                        break;
                    case 1:
                        _carScript.MoveToRoad(0);
                        break;
                }
            }
        }
    }
}
