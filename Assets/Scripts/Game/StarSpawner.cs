using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Game {

    public class StarSpawner : MonoBehaviour {

		[Serializable]
        public class Pool {

            public string tag;
            public GameObject prefab;
            public int size;
        }

        [SerializeField] 
        private EventListener _updateEventListener;
        
		[SerializeField] 
        private List<Pool> _pools;
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