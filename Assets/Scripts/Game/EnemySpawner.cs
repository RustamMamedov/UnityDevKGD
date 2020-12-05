using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Sirenix.OdinInspector;
using System.Linq;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [Header("Вражины в игре")]
        [SerializeField]
        [ValidateInput(nameof(CheckDuplicates), "Value is invalid for 'Car Prefabs'")]
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

        [SerializeField]
        private ScriptableBoolValue _hardGameMode;

        [SerializeField]
        private int _initialPoolNumber = 1;

        private enum CarType {
            family,
            truck,
            SUV,
        }

        private List<(CarType type, GameObject car)> _cars = new List<(CarType, GameObject)>();
        private Dictionary<CarType, Stack<GameObject>> _stack;
        private float _currentTimer;

        private bool CheckDuplicates() {
            return _carPrefabs.Distinct().Count() == _carPrefabs.Count;
        }

        private void Awake() {
            if (_hardGameMode.value) {
                _spawnCooldown /= 2.5f;
            }
        }

        private void Start() {
            GeneratePool();
        }

        private void OnEnable() {
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappend += UpdateBehaviour;
            _carCollisionListener.OnEventHappend += OnCarCollision;
        }

        private void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappend -= UpdateBehaviour;
            _carCollisionListener.OnEventHappend -= OnCarCollision;
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

        private void GeneratePool() {
            _stack = new Dictionary<CarType, Stack<GameObject>> {
                [CarType.family] = new Stack<GameObject>(),
                [CarType.truck] = new Stack<GameObject>(),
                [CarType.SUV] = new Stack<GameObject>(),
            };
            for (int i = 0; i < _initialPoolNumber; i++) {
                _stack[CarType.family].Push(CreateCar(CarType.family));
                _stack[CarType.truck].Push(CreateCar(CarType.truck));
                _stack[CarType.SUV].Push(CreateCar(CarType.SUV));
            }
        }

        private GameObject CreateCar(CarType type) {
            var car = Instantiate(_carPrefabs[(int)type], Vector3.zero, Quaternion.Euler(0f, 180f, 0f));
            car.SetActive(false);
            return car;
        }

        private void SpawnCar() {
            var type = (CarType)UnityEngine.Random.Range(0, _carPrefabs.Count);
            var car = GetCarFromPool(type);

            var road = UnityEngine.Random.Range(-1, 2);
            var position = new Vector3(1f * road * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            car.transform.position = position;

            _cars.Add((type, car));
        }

        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i >= 0; i--) {
                if (_playerPositionZ.value - _cars[i].car.transform.position.z > _distanceToPlayerToDestroy) {
                    SetCarToPool(_cars[i].type, _cars[i].car);
                    _cars.RemoveAt(i);
                }
            }
        }

        private GameObject GetCarFromPool(CarType type) {
            if (_stack[type].Count == 0) {
                _stack[type].Push(CreateCar(type));
            }
            var car = _stack[type].Pop();
            car.SetActive(true);
            return car;
        }

        private void SetCarToPool(CarType type, GameObject car) {
            car.SetActive(false);
            _stack[type].Push(car);
        }
    }
}