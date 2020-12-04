using System;
using System.Collections.Generic;
using Events;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [Serializable]
        public class Pool {

            public string tag;
            public GameObject prefab;
            public int size;
        }

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [ValidateInput(nameof(ListHasDuplicates), "List has duplicates")]
        [SerializeField] 
        private List<Pool> _pools;

        [SerializeField]
        private float _distanceToPlayerToSpawn;

        [SerializeField]
        private float _distanceToPlayerToDestroy;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;
        
        [SerializeField] 
        private ScriptableBoolValue _isHardScriptableBoolValue;
        
        private float _currentTimer;
        private float _spawnCooldown;
        private Dictionary<string, Queue<GameObject>> _poolDictionary;
        
        private void OnEnable() {
            SubscribeToEvents();
            SetDifficulty();
        }

        private void Start() {
            GeneratePool();
        }
        
        private void OnDisable() {
            UnsubscribeToEvents();
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
        
        private void SetDifficulty() {
            if (_isHardScriptableBoolValue.value) {
                _spawnCooldown = 1f;
            }
            else {
                _spawnCooldown = 5f;
            }
        }
        
        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionListener.OnEventHappened += OnCarCollision;
        }

        private void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {
            UnsubscribeToEvents();
        }

        private void UpdateBehaviour() {
            _currentTimer += Time.deltaTime;
            if (_currentTimer < _spawnCooldown) {
                return;
            }
            _currentTimer = 0f;

            SpawnCar();
        }

        private void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            var randomCar = Random.Range(0, _pools.Count);
            var tag = _pools[randomCar].tag;
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var car = GetFromPool(tag, position, Quaternion.Euler(0f, 180f, 0f));
            if (car != null) {
                car.SetActive(true);
            }
        }

        private bool ListHasDuplicates(List<Pool> list) {
            var hasDuplicates = false;

            var set = new HashSet<Pool>();
            foreach (var pool in list) {
                if (!set.Add(pool)) {
                    hasDuplicates = true;
                    break;
                }
            }
            
            return !hasDuplicates;
        }
    }
}