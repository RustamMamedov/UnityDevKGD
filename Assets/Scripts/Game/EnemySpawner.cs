using UnityEngine;
using Events;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UI;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        [ValidateInput(nameof(ValidateCarPrefs), "Was added two equal car prefabs")]
        private List<GameObject> _carPrefabs = new List<GameObject>();

        [SerializeField]
        private List<CarSettings> _carSettings = new List<CarSettings>();

        [SerializeField]
        private float _spawnCooldown;

        [SerializeField]
        private float _distanceToPlayerToSpawn;

        [SerializeField]
        private float _distanceToPlayerToDestroy;

        [SerializeField]
        private int _initialStackCarNumber;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        [SerializeField]
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private ScriptableIntValue[] _dodgeScores;

        private float _currentTimer;

        private List<Transform> _cars = new List<Transform>();

        private List<int> _carsIndexs = new List<int>();

        private List<Stack<Transform>> _stacks;

        private void OnEnable() {
            GeneratePool();
            bool isHard = SettingsScreen.GetInstance().getGamemode();
            if (isHard) {
                _spawnCooldown /= 2;
            }
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

        private void SpawnCar() {
            if (_cars.Count < _initialStackCarNumber) {
                var car = GetCarFromStack();
                _cars.Add(car);
            }
        }

        private GameObject CreateCar(int carIndex) {
            var car = Instantiate(_carPrefabs[carIndex], Vector3.zero, Quaternion.Euler(0f, 180f, 0f));
            car.SetActive(false);
            return car;
        }

        private void GeneratePool() {
            _stacks = new List<Stack<Transform>>();
            
            for (int i = 0; i < 3; i++) {
                Stack<Transform> stack = new Stack<Transform>();
                _stacks.Add(stack);
                for (int j = 0; j < _initialStackCarNumber; j++) {
                    _stacks[i].Push(CreateCar(i).transform);
                }
            }
        }

        private Transform GetCarFromStack() {
            var randomCar = Random.Range(0, 3);
            var car = _stacks[randomCar].Pop();
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            car.position = position;
            car.gameObject.SetActive(true);
            return car;
        }

        private void SetCarToStack(Transform car) {
            car.gameObject.SetActive(false);
            var randomCar = Random.Range(0, 3);
            _stacks[randomCar].Push(car);
        }

        private void MoveCarToStack() {
            if (_cars.Count == 0) {
                return;
            }
            var firstCar = _cars[0];
            _cars.RemoveAt(0);
            SetCarToStack(firstCar);
        }

        private void HandleCarsBehindPlayer() {
            if (_cars.Count == _initialStackCarNumber && _playerPositionZ.value - _cars[0].position.z > _distanceToPlayerToDestroy) {
                MoveCarToStack();
            }
        }

        private bool ValidateCarPrefs(List<GameObject> prefs) {

            foreach (var item in prefs) {
                List<GameObject> tempList = prefs.FindAll(
                    delegate (GameObject it) {
                        return it.name == item.name;
                    } );
                if (tempList.Count > 1) {
                    return false;
                }
            }
            return true;
        }

    }
}