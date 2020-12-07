using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class StarSpawner : MonoBehaviour {

        [SerializeField]
        private GameObject _star;

        [SerializeField]
        private ScriptableIntValue _spawnPosition;

        [SerializeField]
        private GameObject _car;


        private void SetStarSpawnPosition() {
            if(_spawnPosition == 1) {
                _star.transform.position = _car.transform.position;
            }
            if(_spawnPosition == 2) {
                _star.transform.position = _car.transform.position + _car.width();
            }
        }

    }
}