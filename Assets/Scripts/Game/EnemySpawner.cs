using System.Collections.Generic;
using Events;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        [ValidateInput(nameof(ValidatePrefabs))]
        private List<GameObject> _carPrefabs;

        [SerializeField]
        private float _spawnCooldown;

        [SerializeField]
        private float _distanceToPlayerToSpawn;

        [SerializeField]
        private float _distanceToPlayerToDestroy;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        private float _currentTimer;
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
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var randomCar = _carPrefabs[Random.Range(0, _carPrefabs.Count)];
            var car = Instantiate(randomCar, position, Quaternion.Euler(0f, 180f, 0f));

            _cars.Add(car);
        }

        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    Destroy(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }

        private bool ValidatePrefabs() {
            bool isCorrect = true;

            for (int i = 0; i < _carPrefabs.Count - 1; i++) {
                for (int j = i + 1; j < _carPrefabs.Count; j++) {
                    if (_carPrefabs[i] == _carPrefabs[j]) {
                        isCorrect = false;
                        break;
                    }
                }
            }

            return isCorrect;
        }
    }
}