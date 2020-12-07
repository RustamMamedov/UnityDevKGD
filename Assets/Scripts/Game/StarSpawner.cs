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
        private float _distanceToPlayerToSpawn;
        
        private float _currentTimer;
        private float _spawnCooldown;
        private Dictionary<string, Queue<GameObject>> _poolDictionary;

		private void OnEnable() {
           
        }

        private void Start() {
            SubscribeToEvents();
            GeneratePool();
        }
        
        private void OnDisable() {
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
        }

        private void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
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

        private void UpdateBehaviour() {
            
        }
    }
}