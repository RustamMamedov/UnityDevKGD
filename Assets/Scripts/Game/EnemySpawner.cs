using System.Collections.Generic;
using Events;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _gameSavedEventListener;

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
        private GameScreen _gameScreen;

        [ValidateInput(nameof(ValidateCarsInput), "Values in CarPrefabs repeats", InfoMessageType.Error)]
        [SerializeField]
        private List<GameObject> _carPrefabs = new List<GameObject>();

        private float _currentTimer = 0f;
        private float _easyDifficultySpawnCooldown = 3f;
        private float _hardDifficultySpawnCooldown = 1f;

        private List<GameObject> _cars = new List<GameObject>();

        private void OnEnable() {
            SubscribeToEvents();
            SetDifficulty();
        }

        private void OnDisable() {
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents() {
            _gameSavedEventListener.OnEventHappened += SetDifficulty;
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionListener.OnEventHappened += OnCarCollision;
        }

        private void UnsubscribeToEvents() {
            _gameSavedEventListener.OnEventHappened -= SetDifficulty;
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionListener.OnEventHappened -= OnCarCollision;
        }

        private void SetDifficulty() {
            switch (Save.Settings.difficulty) {
                case Save.SavedSettings.Difficulty.Hard:
                    _spawnCooldown = _hardDifficultySpawnCooldown;
                    break;
                case Save.SavedSettings.Difficulty.Easy:
                    _spawnCooldown = _easyDifficultySpawnCooldown;
                    break;
            }
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

        private bool ValidateCarsInput(List<GameObject> carPrefabs) {
            for (int i = 0; i < _carPrefabs.Count - 1; i++) {
                for (int j = i + 1; j < _carPrefabs.Count; j++) {
                    if (_carPrefabs[i].name.Equals(_carPrefabs[j].name)) {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
