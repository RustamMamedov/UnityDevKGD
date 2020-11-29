using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Sirenix.OdinInspector;

namespace Game {
    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        [ValidateInput(nameof(ValidateCarPrefabs))]
        private List<GameObject> _carPrefabs = new List<GameObject>();

        [SerializeField]
        private float _spawnCooldownEasy;

        [SerializeField]
        private float _spawnCooldownHard;

        [SerializeField]
        private float _distanceToPlayerToSpawn;

        [SerializeField]
        private float _distanceToPlayerToDestroy;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        private float _currentTimer;

        private float _spawnCooldown;

        private List<GameObject> _cars = new List<GameObject>();
        private void OnEnable() {

            if(PlayerPrefs.GetInt(DataKeys.DIFFICULT_KEY) == 0) {
                _spawnCooldown = _spawnCooldownEasy;
            } else {
                _spawnCooldown = _spawnCooldownHard;
            }
            SubscribeEvents();
        }

        private void OnDisable() {
            UnsubscribeEvents();
        }

        private void SubscribeEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionListener.OnEventHappened += OnCarCollision;
        }

        private void UnsubscribeEvents() {
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
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var randomCar = Random.Range(0, _carPrefabs.Count);
            var car = Instantiate(_carPrefabs[randomCar], position, Quaternion.Euler(0f, 180f, 0f));
            _cars.Add(car);
        }

        private void HandleCarsBehindPlayer() {
            for(int i = _cars.Count - 1; i > -1; i--) {
                if(_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    Destroy(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }
        private void OnCarCollision() {
            UnsubscribeEvents();
        }

        private bool ValidateCarPrefabs() {
            for (int i = 0; i < _carPrefabs.Count - 1; i++) {
                for (int j = i + 1; j < _carPrefabs.Count; j++) {
                    if (_carPrefabs[i] == _carPrefabs[j]) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
