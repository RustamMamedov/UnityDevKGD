using Events;
using System.Collections.Generic;
using UnityEngine;
using Values;

namespace Game {
    
    public class EnemySpawner : MonoBehaviour {

        // Fields.

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private GameObject _enemyPrefab;

        [SerializeField]
        private float _spawnCooldown;

        [SerializeField]
        private float _spawnDistance;

        [SerializeField]
        private float _despawnDistance;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZValue;

        [SerializeField]
        private ScriptableFloatValue _laneWidth;

        private float _currentTimer = 0;

        private List<GameObject> _cars = new List<GameObject>();


        // Life cycle.

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
        }

        private void UnsubscribeFromEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
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


        // Car spawning.

        private void SpawnCar() {
            int randomRoad = Random.Range(-1, 2);
            var position = new Vector3(randomRoad * _laneWidth.value, 0f, _playerPositionZValue.value + _spawnDistance);
            var car = Instantiate(_enemyPrefab, position, Quaternion.Euler(0, 180, 0), transform);
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
