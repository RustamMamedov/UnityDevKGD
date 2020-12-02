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
        private EventListener _carDodgeEventListener;

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

        private int _numberOfCarsInStack = 3;

        private float _currentTimer = 0f;
        private float _easyDifficultySpawnCooldown = 3f;
        private float _hardDifficultySpawnCooldown = 1f;

        private Dictionary<CarSettings.CarType, Stack<GameObject>> _cars;
        private Queue<GameObject> _spawnedCars;

        private void OnEnable() {
            SubscribeToEvents();
            SetDifficulty();
            GeneratePool();

            _spawnedCars = new Queue<GameObject>();
        }

        private void OnDisable() {
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents() {
            _gameSavedEventListener.OnEventHappened += SetDifficulty;
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionListener.OnEventHappened += OnCarCollision;
            _carDodgeEventListener.OnEventHappened += HandleCarsBehindPlayer;
        }

        private void UnsubscribeToEvents() {
            _gameSavedEventListener.OnEventHappened -= SetDifficulty;
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionListener.OnEventHappened -= OnCarCollision;
            _carDodgeEventListener.OnEventHappened -= HandleCarsBehindPlayer;
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

            _currentTimer += Time.deltaTime;

            if (_currentTimer < _spawnCooldown) {
                return;
            }

            _currentTimer = 0f;

            SpawnCar();
        }

        private void HandleCarsBehindPlayer() {
            var car = _spawnedCars.Dequeue();
            car.TryGetComponent<Car>(out var carScript);
            car.SetActive(false);
            _cars[carScript.carSettings.carType].Push(car);
        }

        private void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            var randomCarIndex = Random.Range(0, 3);
            var position = new Vector3(randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);

            var car = _cars[(CarSettings.CarType) randomCarIndex].Pop();
            car.transform.position = position;
            car.SetActive(true);
            _spawnedCars.Enqueue(car);

        }

        private void GeneratePool() {
            _cars = new Dictionary<CarSettings.CarType, Stack<GameObject>>();

            foreach (GameObject car in _carPrefabs) {
                car.TryGetComponent<Car>(out var carScript);
                _cars.Add(carScript.carSettings.carType, new Stack<GameObject>());

                for (int i = 0; i < _numberOfCarsInStack; i++) {
                    var spawnedCar = Instantiate(car, new Vector3(0, 0, -10), Quaternion.Euler(0, 180, 0));
                    spawnedCar.SetActive(false);
                    _cars[carScript.carSettings.carType].Push(spawnedCar);
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
