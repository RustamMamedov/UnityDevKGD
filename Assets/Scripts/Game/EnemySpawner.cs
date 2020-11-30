using Events;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        private EventDispatcher _carDodgeDispatcher;

        [SerializeField]
        [ValidateInput(nameof(CarCheck))]
        private List<GameObject> _carPrefabs = new List<GameObject>();

        [SerializeField]
        private float _spawnCooldown = 0;

        [SerializeField]
        private float _cooldownForEasyMode = 7f;

        [SerializeField]
        private float _cooldownForHardMode = 3f;

        [SerializeField]
        private float _distanceToPlayerToSpawn;

        [SerializeField]
        private int _distanceToDodge = 5;

        [SerializeField]
        private float _distanceToPlayerToDestroy;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        private float _currentTimer = 0f;
        private List<GameObject> _cars = new List<GameObject>();

        private void OnEnable() {
            if (PlayerPrefs.GetInt(DataKeys.DIFFICULT_KEY) == 0) {
                _spawnCooldown = _cooldownForEasyMode;
            } else {
                _spawnCooldown = _cooldownForHardMode;
            }
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnSubscribeToEvents();
        }

        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionListener.OnEventHappened += OnCarCollision;
        }
        private void UnSubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {
            UnSubscribeToEvents();
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
            var enemyCarIndex = Random.Range(0, 3);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var car = Instantiate(_carPrefabs[enemyCarIndex], position, Quaternion.Euler(0f, 180f, 0f));
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

        private bool CarCheck() {
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

