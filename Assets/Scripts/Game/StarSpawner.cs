using System;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Audio;
using Random = UnityEngine.Random;

namespace Game {

    public class StarSpawner : MonoBehaviour {

		[Serializable]
        public class Pool {

            public string tag;
            public GameObject prefab;
            public int size;
        }

        [SerializeField] 
        private EventListener _starGotCollectedEventListener;
        
        [SerializeField] 
        private EventListener _carGorSpawnedEventListener;

        [SerializeField]
        private AudioSourcePlayer _audioSourcePlayer;
        
		[SerializeField] 
        private List<Pool> _pools;
        
        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        [SerializeField]
        private float _distanceToEnemyToSpawn;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;
       
        [SerializeField]
        private ScriptableVectorValue _enemyCarSpawnPosition;
        
        private Dictionary<string, Queue<GameObject>> _poolDictionary;

		private void OnEnable() {
            SubscribeToEvents();
        }

        private void Start() {
            GeneratePool();
        }
        
        private void OnDisable() {
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents() {
            _carGorSpawnedEventListener.OnEventHappened += SpawnStar;
            _starGotCollectedEventListener.OnEventHappened += PlaySound;
        }

        private void UnsubscribeToEvents() {
            _carGorSpawnedEventListener.OnEventHappened -= SpawnStar;
            _starGotCollectedEventListener.OnEventHappened -= PlaySound;
        }

        private void PlaySound() {
            _audioSourcePlayer.Play();
        }
        
        public void GeneratePool() {
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();
            
            foreach (var pool in _pools) {
                Queue<GameObject> objectPool = new Queue<GameObject>();
                
                for (int i = 0; i < pool.size; i++) {
                    var obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }
                
                _poolDictionary.Add(pool.tag, objectPool);
            }
        }

        private GameObject GetFromPool(string tag, Vector3 position, Quaternion rotation) {
            if (!_poolDictionary.ContainsKey(tag) || _poolDictionary[tag].Peek().activeSelf) {
                return null;
            }

            var objectToSpawn = _poolDictionary[tag].Dequeue();
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            
            _poolDictionary[tag].Enqueue(objectToSpawn);
            return objectToSpawn;
        }
        
        private void SpawnStar() {
            var beforeOrAfter = Random.Range(-1, 1);
            var tag = _pools[0].tag;
            var position = _enemyCarSpawnPosition.value;
            position.y = 1;
            if (beforeOrAfter == 0) {
                position.z += _distanceToEnemyToSpawn;
            }
            else {
                position.z += -1f * _distanceToEnemyToSpawn;
            }
            var star = GetFromPool(tag, position, Quaternion.Euler(0f, 180f, 0f));
            if (star != null) {
                star.SetActive(true);
            }
        }
    }
}