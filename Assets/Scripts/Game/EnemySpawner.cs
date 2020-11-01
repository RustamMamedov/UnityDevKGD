using System;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Random = UnityEngine.Random;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField] private EventListener _updateEventListener;

        [SerializeField] private EventListener _carCollisionListener;

        [SerializeField] private GameObject _carPrefab;

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
            var position = new Vector3(randomRoad * 1f * _roadWidth.value, 0,
                _playerPositionZ.value + _distanceToPlayerToSpawn);
            var car = Instantiate(_carPrefab, position, Quaternion.Euler(0, 180, 0));
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

    }
}