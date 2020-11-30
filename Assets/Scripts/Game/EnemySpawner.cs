using Events;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Values;

namespace Game {
    
    public class EnemySpawner : MonoBehaviour {

        // Fields.

        [BoxGroup("Event listeners")]
        [SerializeField]
        private EventListener _updateEventListener;

        [BoxGroup("Event listeners")]
        [SerializeField]
        private EventListener _carCollisionEventListener;

        [BoxGroup("Event listeners")]
        [SerializeField]
        private EventListener _difficultyChangedEventListener;

        [BoxGroup("Spawn properties")]
        [ValidateInput(nameof(ValidateEnemyPrefabs), "Multiple prefabs in the list are the same!", InfoMessageType.Error)]
        [SerializeField]
        private List<GameObject> _enemyPrefabs;

        private bool ValidateEnemyPrefabs(List<GameObject> enemyPrefabs) {
            return new HashSet<GameObject>(enemyPrefabs).Count == enemyPrefabs.Count;
        }

        [BoxGroup("Scriptable values")]
        [SerializeField]
        private ScriptableFloatValue _playerPositionZValue;

        [BoxGroup("Scriptable values")]
        [SerializeField]
        private ScriptableFloatValue _laneWidth;

        [BoxGroup("Scriptable values")]
        [SerializeField]
        private ScriptableIntValue _difficultyValue;

        [BoxGroup("Spawn properties")]
        [SerializeField]
        private float _normalSpawnCooldown;

        [BoxGroup("Spawn properties")]
        [SerializeField]
        private float _hardSpawnCooldown;

        [BoxGroup("Spawn properties")]
        [SerializeField]
        private float _spawnDistance;

        [BoxGroup("Spawn properties")]
        [SerializeField]
        private float _despawnDistance;

        private float _currentTimer = 0;

        private float _spawnCooldown;

        private List<GameObject> _cars = new List<GameObject>();


        // Life cycle.

        private void Awake() {
            UpdateCooldown();
        }

        private void OnEnable() {
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnsubscribeFromEvents();
        }


        // Event handlers.

        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
            _difficultyChangedEventListener.OnEventHappened += UpdateCooldown;
        }

        private void UnsubscribeFromEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
            _difficultyChangedEventListener.OnEventHappened -= UpdateCooldown;
        }

        private void UpdateBehaviour() {
            _currentTimer += Time.deltaTime;
            if (_currentTimer >= _spawnCooldown) {
                _currentTimer = 0;
                SpawnCar();
            }
            HandleDespawnableCars();
        }

        private void OnCarCollision() {
            UnsubscribeFromEvents();
        }

        private void UpdateCooldown() {
            int difficulty = _difficultyValue.value;
            if (difficulty == 0) {
                _spawnCooldown = _normalSpawnCooldown;
            } else {
                _spawnCooldown = _hardSpawnCooldown;
            }
        }


        // Car spawning.

        private void SpawnCar() {
            if (_enemyPrefabs.Count == 0) {
                return;
            }
            int randomRoad = Random.Range(-1, 2);
            var position = new Vector3(randomRoad * _laneWidth.value, 0f, _playerPositionZValue.value + _spawnDistance);
            int randomEnemy = Random.Range(0, _enemyPrefabs.Count);
            var car = Instantiate(_enemyPrefabs[randomEnemy], position, Quaternion.Euler(0, 180, 0), transform);
            _cars.Add(car);
        }

        private void HandleDespawnableCars() {
            for (int i = _cars.Count - 1; i >= 0; --i) {
                if (_playerPositionZValue.value - _cars[i].transform.position.z >= _despawnDistance) {
                    Destroy(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }


    }

}
