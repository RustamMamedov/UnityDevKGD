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
        private int _maxCarsInStack = 3;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        private int _difficulty;
        private float _currentTimer;
        private List<List<Transform>> _cars = new List<List<Transform>>();
        private List<Stack<Transform>> _carStacks = new List<Stack<Transform>>();

        private void Start() {
            for (int i = 0; i < _carPrefabs.Count; i++) {
                GenerateCarPool(_carPrefabs[i]);
                _cars.Add(new List<Transform>());
            }
        }

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

        public void SetDifficulty(float difficulty) {
            float difficultyCoefficient = 1f / (difficulty + 1);
            _spawnCooldown *= difficultyCoefficient;
            _maxCarsInStack += ((int) difficulty * 2);
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

            AddNewCar();
        }

        private void AddNewCar() {
            var usedRoad = -2;
            var randomRoad = Random.Range(-1, 2);

            for (int i = 0; i < Random.Range(1, 3); i++) {
                while (randomRoad == usedRoad) {
                    randomRoad = Random.Range(-1, 2);
                }
                usedRoad = randomRoad;

                SpawnCar(randomRoad);
            }
        }

        private void SpawnCar(int road) {
            var carIndex = Random.Range(0, _carPrefabs.Count);
            var car = GetCarFromStack(carIndex);

            car.position = new Vector3(1f * road * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            _cars[carIndex].Add(car);
        }

        private void HandleCarsBehindPlayer() {
            for (int i = 0; i < _cars.Count; i++) {
                for (int j = _cars[i].Count - 1; j > -1; j--) {
                    if (_playerPositionZ.value - _cars[i][j].transform.position.z > _distanceToPlayerToDestroy) {
                        SetCarToStack(i, _cars[i][j]);
                        _cars[i].RemoveAt(j);
                    }
                }
            }
        }

        private void SetCarToStack(int index, Transform car) {
            car.gameObject.SetActive(false);
            _carStacks[index].Push(car);
        }

        private void GenerateCarPool(GameObject carPrefab) {
            var carStack = new Stack<Transform>();

            for (int i = 0; i < _maxCarsInStack; i++) {
                carStack.Push(CreateCar(carPrefab).transform);
            }

            _carStacks.Add(carStack);
        }

        private Transform GetCarFromStack(int index) {
            if (_carStacks[index].Count == 0) {
                _carStacks[index].Push(CreateCar(_carPrefabs[index]).transform);
            }

            var car = _carStacks[index].Pop();
            car.gameObject.SetActive(true);

            return car;
        }

        private GameObject CreateCar(GameObject carPrefab) {
            var car = Instantiate(carPrefab, Vector3.zero, Quaternion.Euler(0f, 180f, 0f));
            car.SetActive(false);

            return car;
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