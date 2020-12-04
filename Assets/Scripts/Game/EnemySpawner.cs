using Events;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        [ValidateInput(nameof(VolidateList))]
        private List<GameObject> _carPrefab = new List<GameObject>();

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
        private ScriptableIntValue _slider;

        private float _currentTimer;
        private List<CarAndType> _cars = new List<CarAndType>();

        private List<Stack<GameObject>> _carStack;

        [SerializeField]
        private int _countStack;

        private struct CarAndType {
            public GameObject car;
            public int type;

            public CarAndType(GameObject c, int t){
                car = c;
                type = t;
            }
        }

        //функция создания новых машинок
        private GameObject CreateCar(GameObject prefsCars) {
            return Instantiate(prefsCars, Vector3.zero, Quaternion.Euler(0f, 180f, 0f));
        }

        private GameObject GetFromStack(int count) {
            if (_carStack[count].Count > 0) {
                var tmp = _carStack[count].Pop();
                tmp.SetActive(true);
                return tmp;
            }else {
                return CreateCar(_carPrefab[count]);
            }
        }

        private void PutInStack(CarAndType inCar) {
            inCar.car.SetActive(false);
            _carStack[inCar.type].Push(inCar.car);
        }

        private void Awake() {
            _carStack = new List<Stack<GameObject>>(); //пустой новый экземпляр
            for (int i = 0; _carPrefab.Count > i; i++) {
                var emptyCar = new Stack<GameObject> ();
                _carStack.Add(emptyCar);
                for (int j = 0; _countStack > j; j++) {
                    var tmp = CreateCar(_carPrefab[i]);
                    tmp.SetActive(false);
                    _carStack[i].Push(tmp);
                }
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

        public void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var randomCar = Random.Range(0, _carPrefab.Count);
            var car = GetFromStack(randomCar);
            car.transform.position = position;
            var carAndTypeStruck = new CarAndType(car, randomCar);
            _cars.Add(carAndTypeStruck);
            
            //рандом двух машинок при режиме hard
            if (_slider.value == 1f) {
                if (Random.Range(0, 2) % 2 == 0) {
                    /*int randomRoad2;
                    do {
                        randomRoad2 = Random.Range(-1, 2);
                    } while (randomRoad == randomRoad2);*/
                    var rand = Random.Range(1, 3);
                    int randomRoad2 = randomRoad == 0 ? rand * 2 - 3 : randomRoad * (1 - rand);
                    var randomCar2 = Random.Range(0, _carPrefab.Count);
                    var car2 = GetFromStack(randomCar2);
                    var position2 = new Vector3(randomRoad2 * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn + Random.Range(-_distanceToPlayerToSpawn * 0.4f, _distanceToPlayerToSpawn * 0.4f));
                    car2.transform.position = position2;
                    var carAndTypeStruck2 = new CarAndType(car2, randomCar2);
                    _cars.Add(carAndTypeStruck2);

                }
            }
        }

        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].car.transform.position.z > _distanceToPlayerToDestroy) {
                    PutInStack(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }

        /*public void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var randomCar = Random.Range(0, _carPrefab.Count);
            var car = Instantiate(_carPrefab[randomCar], position, Quaternion.Euler(0f, 180f, 0f));
            _cars.Add(car);

            //рандом двух машинок при режиме hard
            if (_slider.value == 1f) {
                if (Random.Range(0, 2) % 2 == 0) {
                    int randomRoad2;
                    do {
                        randomRoad2 = Random.Range(-1, 2);
                    } while (randomRoad == randomRoad2);
                    var position2 = new Vector3(randomRoad2 * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn + Random.Range(-_distanceToPlayerToSpawn * 0.4f, _distanceToPlayerToSpawn * 0.4f));
                    var car2 = Instantiate(_carPrefab[Random.Range(0, _carPrefab.Count)], position2, Quaternion.Euler(0f, 180f, 0f));
                    _cars.Add(car2);

                }
            }
        }

        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    Destroy(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }*/

        private bool VolidateList() {
            for (var i = 0; _carPrefab.Count - 1 > i; i++) {
                for (var j = i + 1; _carPrefab.Count > j; j++) {
                    if (_carPrefab[i] == _carPrefab[j]) {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}