using UnityEngine;
using Events;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Game {
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

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
        private ScriptableIntValue _complexity;

        private float _currentTimer;
        private List<CarType> _cars = new List<CarType>();

        [ValidateInput(nameof(ValidationListCarPrefabs))]
        [SerializeField]
        private List<GameObject> _carPrefab = new List<GameObject>();

        [SerializeField]
        private List<Stack<GameObject>> _stackCar = new List<Stack<GameObject>>();

        [SerializeField]
        private int _nomer;//количесво элементов находящ в каждом стеке

        private struct CarType {
            public GameObject carType;
            public int lastType;
            public CarType(GameObject car, int number) {
                carType = car;
                lastType = number;
            }
        }

        private GameObject CreateCar(GameObject mashina) {
            return Instantiate(mashina, Vector3.zero, Quaternion.Euler(0f, 180f, 0f));
        }

        private GameObject GoBack(int nomer) {
            if(_stackCar[nomer].Count > 0) {
                var car = _stackCar[nomer].Pop();
                car.SetActive(true);
                return car;
            }
            else {
                return CreateCar(_carPrefab[nomer]);
            }
        }

        private void BackInStack(GameObject car, int nomer) {
            car.SetActive(false);
            _stackCar[nomer].Push(car);
        }

        private bool ValidationListCarPrefabs() {
            for (int i = 0; i<_carPrefab.Count; i++) {
                if (i != _carPrefab.LastIndexOf(_carPrefab[i])) {
                    return false;
                }
            }
            return true;
        }

        private void Awake() {
            for (int i = 0; i< _carPrefab.Count; i++) {
                var t = new Stack<GameObject>();
                _stackCar.Add(t);
                for (int j = 0; j < _nomer; j++) {
                    var n = CreateCar(_carPrefab[i]);
                    n.SetActive(false);
                    _stackCar[i].Push(n);
                }
            }
        }

        private void OnEnable() {
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnSubscribeToEvents();

        }

        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBahaviour;
            _carCollisionListener.OnEventHappened += OnCarCollision;
        }

        private void UnSubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBahaviour;
            _carCollisionListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {
            UnSubscribeToEvents();
        }

        private void UpdateBahaviour() {
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
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value+_distanceToPlayerToSpawn);
            var randomCar = Random.Range(0, _carPrefab.Count);
            //var car = Instantiate(_carPrefab[randomCar], position, Quaternion.Euler(0f, 180f, 0f));
            //_cars.Add(car);
            var car = GoBack(randomCar);
            car.transform.position = position;
            var carStrukt = new CarType(car, randomCar);
            _cars.Add(carStrukt);
            if (_complexity.value == 1) {
               if (Random.Range(0, 2) % 2 == 0) {
                    /*     int randomRoad2;
                        do {
                            randomRoad2 = Random.Range(-1, 2);
                        } while (randomRoad == randomRoad2);*/
                    int random = Random.Range(1, 3);
                    var randomRoad2 = (randomRoad == 0) ? random * 2 - 3 : randomRoad * (1-random);
                    var randomCar2 = Random.Range(0, _carPrefab.Count);
                    var position2 = new Vector3((float)randomRoad2 * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn + Random.Range(-_distanceToPlayerToSpawn * 0.4f, _distanceToPlayerToSpawn * 0.4f));
                    var car2 = GoBack(randomCar2);
                    car2.transform.position = position2;
                    var carStrukt2 = new CarType(car2, randomCar2);
                    _cars.Add(carStrukt2);
                }
            }
            
        }

        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].carType.transform.position.z > _distanceToPlayerToDestroy) {
                    BackInStack(_cars[i].carType, _cars[i].lastType);
                    _cars.RemoveAt(i);
                }
            }
        }
    }
}
