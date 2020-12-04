using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Events;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListeners _updateEventListener;

        [SerializeField]
        private EventListeners _carCollisionEventLister;

        [ValidateInput(nameof(ValidationListEnemyCars))]

        [SerializeField]
        private List<GameObject> _carPrefabs;

        [SerializeField]
        private ScriptableIntValue _difficultyGame;

        private bool ValidationListEnemyCars(List<GameObject> carPrefabs) {
            for (int i = 0; i < carPrefabs.Count; i++) {
                if (i != carPrefabs.LastIndexOf(carPrefabs[i])) {
                    return false;
                }
            }
            return true;
        }

        [SerializeField]
        private float _spawnCooldawn;

        [SerializeField]
        private float _distanceToPlaySpawn;

        [SerializeField]
        private float _distanceToPlayDestrou;

        [SerializeField]
        private ScriptableFloatValue _playerDistanseZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;
     
        private List<GameObject> _carsActive = new List<GameObject>();

        private List<int> _carsActiveIndexStack = new List<int>();

        private List<Stack<GameObject>> _cars;

        private float _currentTime;

        private bool _spawn = false;

        private int canEmptyRoad = -2;

        private int _carsSpawn = 2;

        private void Awake() {
            GenerateEnemyCarsStecks();
        }

        private void OnDestroy() {
            DleteEnemyCarsStacks();
        }

        private void OnEnable() {
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehavour;
            _carCollisionEventLister.OnEventHappened += OnCarCollision;
        }

        private void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehavour;
            _carCollisionEventLister.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {
            UnsubscribeToEvents();
        }

        private void UpdateBehavour() {
            HendleCarBehaindPlayer();

            _currentTime += Time.deltaTime;
            if (_currentTime < _spawnCooldawn) {
                return;
            }
            if (!_spawn)
            for (int i = 0; i < GetDifficultyGame(); i++) {
                _spawn = true;
                SpawnCar();
            }
            _spawn = false;
            _currentTime = 0;
        }

        private int GetDifficultyGame() {
            if (_difficultyGame.Value == 0) {
                _spawnCooldawn = 3;
                return 1;
            }
            _spawnCooldawn = 1;
            return 2;
        }

        private void SpawnCar() {
            var randomCar =  Random.Range(0, 3);
            while(_cars[randomCar].Count == 0) {
                randomCar = Random.Range(0, 3);
            }
            //if (_cars[randomCar].Count == 0) {
            //    var temp = _cars[randomCar];
            //    _cars[randomCar] = _cars[_cars.Count-1];
            //    _cars[_cars.Count-1] = temp;
            //    randomCar = Random.Range(0, 2);
            //}
            var randomRoad = Random.Range(-1, 2);
            while (canEmptyRoad == randomRoad) {
                randomRoad = Random.Range(-1, 2);
            }
            canEmptyRoad = randomRoad;
            var position = new Vector3(1f * randomRoad * _roadWidth.Value, 0f, _playerDistanseZ.Value + _distanceToPlaySpawn);
            var car = _cars[randomCar].Pop(); 
            car.transform.position=position;
            car.SetActive(true);
            _carsActive.Add(car);
            _carsActiveIndexStack.Add(randomCar);
        }

        private void HendleCarBehaindPlayer() {
            for (int i= _carsActive.Count-1; i>-1;i--) {
                if (_playerDistanseZ.Value - _carsActive[i].transform.position.z > _distanceToPlayDestrou) {
                    _cars[_carsActiveIndexStack[i]].Push(_carsActive[i]);
                    _carsActive[i].SetActive(false);
                    _carsActive.RemoveAt(i);
                    _carsActiveIndexStack.RemoveAt(i);
                }
            }
        }

        private void GenerateEnemyCarsStecks() {
            _cars = new List<Stack<GameObject>>();
            for (int i = 0; i < _carPrefabs.Count; i++) {
                _cars.Add(new Stack<GameObject>());
                for (int j = 0; j < _carsSpawn; j++) {
                    var car = Instantiate(_carPrefabs[i], Vector3.zero, Quaternion.Euler(0f, 180f, 0f));
                    car.SetActive(false);
                    _cars[i].Push(car);
                }
            }
        }

        private void DleteEnemyCarsStacks() {
            for (int i = 0; i < _cars.Count; i++) {
                for (int j = 0; j < _cars[i].Count; j++) {
                    var car = _cars[i].Pop();
                    Destroy(car);
                }
                _cars.RemoveAt(i);
            }
        }
    }
}
