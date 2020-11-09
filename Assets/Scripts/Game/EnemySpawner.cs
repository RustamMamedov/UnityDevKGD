using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        [SerializeField]
        private float _spawnCooldown;

        [SerializeField]
        private float _distanceToPlayerToSpawn;

        [SerializeField]
        private float _distanceToPlayerToDestroy;

        [SerializeField]
        private List<GameObject> _carPrefabs = new List<GameObject>();

        private float _currentTimer = 0f;

        private List<GameObject> _cars = new List<GameObject>();

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
            var randomCarIndex = Random.Range(0, 3);
            var position = new Vector3(randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var car = Instantiate(_carPrefabs[randomCarIndex], position, Quaternion.Euler(0f, 180f, 0f));
            _cars.Add(car);
        }

        private void HandleCarsBehindPlayer() {
            foreach (GameObject car in _cars) {
                if (_playerPositionZ.value - car.transform.position.z >= _distanceToPlayerToDestroy) {
                    Destroy(car);
                    _cars.Remove(car);
                    break;
                }
            }
        }

        private void OnCarCollision() {
            UnsubscribeToEvents();
        }

    }
}
