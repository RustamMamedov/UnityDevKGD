using System.Collections;
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
        private GameObject _carPrefab;

        [SerializeField]
        private float _spawnCoolDown;

        [SerializeField]
        private float _distanceToPlayer;

        [SerializeField]
        private float _distanceToDestroy;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        private List<GameObject> _cars = new List<GameObject>();

        private float _currentTimer;

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
            _currentTimer += Time.deltaTime;
            if (_currentTimer < _spawnCoolDown) {
                return;
            }

            _currentTimer = 0f;

            SpawnCar();
            HandleCarsBehindPlayer();
        }

        private void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayer);
            var car = Instantiate(_carPrefab, position, Quaternion.Euler(0f, 180f, 0f));
            _cars.Add(car);
        }

        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToDestroy) {
                    Destroy(_cars[i]);
                    _cars.Remove(_cars[i]);
                }
            }
        }
    }
}