using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {
    public class EnemySpawner : MonoBehaviour{
        [SerializeField]
        private EventListeners _updateEventListeners;

        [SerializeField]
        private EventListeners _carCollisionListener;

        [SerializeField]
        private List<GameObject> _carPrefab = new List<GameObject>();

        [SerializeField]
        private float _spawnCooldown;

        [SerializeField]
        private float _distanceToPlayerToSpawner;

        [SerializeField]
        private float _distanceToPlayerToDestroy;

        [SerializeField]
        private ScriptableFloatValue _playerPisotionZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        private float _currentTimer;
        private List<GameObject> _cars = new List<GameObject>();

        private void OnEnable() {
            SubscribeToEvent();
        }
        private void OnDisable() {
            UnsubscribeToEvent();
        }

        private void SubscribeToEvent() {
            _updateEventListeners.OnEventHappened += UpdateBehaviour;
            _carCollisionListener.OnEventHappened += OnCarCollision;
        }
        private void UnsubscribeToEvent() {
            _updateEventListeners.OnEventHappened -= UpdateBehaviour;
            _carCollisionListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {
            UnsubscribeToEvent();
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
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPisotionZ.value + _distanceToPlayerToSpawner);
            var randomCar = Random.Range(0, _carPrefab.Count);
            var car = Instantiate(_carPrefab[randomCar], position, Quaternion.Euler(0f, 180f, 0f));
            _cars.Add(car);
        }

         private void HandleCarsBehindPlayer() {
            for(int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPisotionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    Destroy(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }  
    }
}

