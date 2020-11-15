using System;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField] private EventListener _updateEventListener;

        [SerializeField] private EventListener _carCollisionListener;

        [SerializeField] [ValidateInput(nameof(ValidateListItems))]
        private List<GameObject> _carPrefabs;

        [SerializeField] private float _spawnCooldown;

        [SerializeField] private float _distanceToPlayerToSpawn;

        [SerializeField] private float _distanceToPlayerDestroy;

        [SerializeField] private ScriptableFloatValue _playerPositionZ;

        [SerializeField] private ScriptableFloatValue _roadWidth;

        private List<GameObject> _cars = new List<GameObject>();

        private float _currentTimer = 0f;

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
            HandleCarsBehindPlayers();
            _currentTimer += Time.deltaTime;
            if (_currentTimer < _spawnCooldown) {
                return;
            }

            _currentTimer = 0f;

            SpawnCar();
        }

        private void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            var randomCar = Random.Range(0, _carPrefabs.Count);
            var position = new Vector3(randomRoad * 1f * _roadWidth.value, 0,
                _playerPositionZ.value + _distanceToPlayerToSpawn);
            var car = Instantiate(_carPrefabs[randomCar], position, Quaternion.Euler(0, 180, 0));
            _cars.Add(car);
        }

        private void HandleCarsBehindPlayers() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerDestroy) {
                    Destroy(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }

        private bool ValidateListItems() {
            for (int i = 0; i < _carPrefabs.Count - 1; i++) {
                for (int j = i + 1; j < _carPrefabs.Count; j++) {
                    if (_carPrefabs[i].Equals(_carPrefabs[j])) {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}