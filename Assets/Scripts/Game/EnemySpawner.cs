using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game {
    
    public class EnemySpawner : MonoBehaviour {
        
        [SerializeField] 
        private EventListener _updateEventListener;

        [SerializeField] 
        private EventListener _carCollisionListener;

        [SerializeField] 
        [ValidateInput(nameof(ValidateCarPrefabList))]
        private List<GameObject> _carPrefabs;

        [FoldoutGroup("Spawner settings")]
        [SerializeField] 
        private float _easySpawnCooldown;

        [FoldoutGroup("Spawner settings")]
        [SerializeField] 
        private float _hardSpawnCooldown;

        [FoldoutGroup("Spawner settings")]
        [SerializeField]
        private float _distanceToPlayerToSpawn;
        
        [FoldoutGroup("Spawner settings")]
        [SerializeField]
        private float _distanceToPlayerToDestroy;
        
        [FoldoutGroup("Assets")]
        [SerializeField] 
        private ScriptableFloatValue _playerPositionZ;
        
        [FoldoutGroup("Assets")]
        [SerializeField] 
        private ScriptableFloatValue _roadWidth;

        [FoldoutGroup("Assets")]
        [SerializeField] 
        private ScriptableBoolValue _difficultySetting;

        private float _currentTimer;
        private float _spawnCooldown;
        
        private List<GameObject> _cars = new List<GameObject>();

        private void Awake() {
            _spawnCooldown = _difficultySetting.value ? _hardSpawnCooldown : _easySpawnCooldown;
        }

        private void OnEnable() {
            SubscribeToEvents();
        }
        
        private void OnDisable() {
            UnsubscribeToEvents();
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
            HandleCarsBehindPlayer();
            _currentTimer += Time.deltaTime;
            if (_currentTimer < _spawnCooldown) {
                return;
            }

            _currentTimer = 0f;

            SpawnCar();
        }

        private void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, 
                _playerPositionZ.value + _distanceToPlayerToSpawn);

            var randomCarPrefab = _carPrefabs[Random.Range(0, _carPrefabs.Count)];
            var car = Instantiate(randomCarPrefab, position, Quaternion.Euler(0f, 180f, 0f));
            _cars.Add(car);
        }

        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; --i) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    Destroy(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }

        private bool ValidateCarPrefabList() {
            return _carPrefabs.Count == _carPrefabs.Distinct().Count();
        }
    }
}


