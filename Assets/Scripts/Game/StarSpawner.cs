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
        private EventListener _updateEventListener;
        
        [SerializeField] 
        private EventListener _starGotCollectedEventListener;

        [SerializeField]
        private AudioSourcePlayer _audioSourcePlayer;
        
		[SerializeField] 
        private List<Pool> _pools;
        
        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        [SerializeField]
        private float _distanceToPlayerToSpawn;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;
        
        [SerializeField]
        private float _spawnCooldown;
        
        private float _currentTimer;
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
            _starGotCollectedEventListener.OnEventHappened += PlaySound;
        }

        private void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
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
            var randomRoad = Random.Range(-1, 2);
            var tag = _pools[0].tag;
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 1f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var star = GetFromPool(tag, position, Quaternion.Euler(0f, 180f, 0f));
            if (star != null) {
                star.SetActive(true);
            }
        }

        private void UpdateBehaviour() {
            _currentTimer += Time.deltaTime;
            if (_currentTimer < _spawnCooldown) {
                return;
            }
            _currentTimer = 0f;

            SpawnStar();
        }
    }
}