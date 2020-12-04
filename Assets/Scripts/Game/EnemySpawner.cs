using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Game;
using Sirenix.OdinInspector;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [ValidateInput(nameof(ValidatePrefabsList))]
        [SerializeField]
        private List<GameObject> _carPrefab = new List<GameObject>();

        [SerializeField]
        private ScriptableFloatValue _spawnCooldown;

        [SerializeField]
        private float _distanceToPlayerToSpawn;

        [SerializeField]
        private float _distanceToPlayerToDestroy;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        [SerializeField]
        private ScriptableFloatValue _playerLength;

        [SerializeField]
        private List<ScriptableFloatValue> _enemiesLength = new List<ScriptableFloatValue>();

        [SerializeField]
        private List<CarSettings> _enemiesScores = new List<CarSettings>();

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private List<int> _enemiesSettings = new List<int>();
        private int _numberOfPrefab;
        private float _currentTimer;
        private bool _dodged;
        private List<Transform> _cars = new List<Transform>();
        private Dictionary<int,Stack<Transform>> _stack;

        private bool ValidatePrefabsList(List<GameObject> prefabs) {
            for (int i = 0; i < prefabs.Count - 1; i++) {
                for (int j = i + 1; j < prefabs.Count; j++) {
                    if (prefabs[i] == prefabs[j]) {
                        return false;
                    }
                }
            }
            return true;
        }
        private void OnEnable() {
            SubscribeToEvents();
        }
        private void OnDisable() {
            UnsubscribeToEvents();
        }
        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }
        private void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }
        private void UpdateBehaviour() {
            HandleCarsBehindPlayer();
            _currentTimer += Time.deltaTime;
            if (_currentTimer < _spawnCooldown.value) {
                return;
            }
            _currentTimer = 0f;
            GeneratePool();
            SpawnCar();
        }
        private void Dodge(CarSettings carSettings) {
                carSettings.dodgeScoreValue.value++;
        }
        private void SpawnCar() {
            _numberOfPrefab = Random.Range(0, 3);
            var car = GetCarFromStack(_numberOfPrefab);
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            car.position = position;
            _cars.Add(car);
            _dodged = true;
            _enemiesSettings.Add(_numberOfPrefab);
        }
        private GameObject CreateCar(int i) {
            var car = Instantiate(_carPrefab[i], Vector3.zero, Quaternion.Euler(0f,180f,0f));
            car.SetActive(false);
            return car;
        }
        private void GeneratePool() {
            _stack = new Dictionary<int, Stack<Transform>>();
            for(int i=0;i<_carPrefab.Count;i++) {
                _stack[i] = new Stack<Transform>();
                _stack[i].Push(CreateCar(i).transform);
            }
        }
        private Transform GetCarFromStack(int i) {
            if (_stack[i].Count == 0) {
                _stack[i].Push(CreateCar(i).transform);
            }
            var car = _stack[i].Pop();
            car.gameObject.SetActive(true);
            return car;
        }
        private void SetCarToStack(Transform car,int i) {
            car.gameObject.SetActive(false);
            _stack[i].Push(car);
        }
        private void MoveCarToStack(int index, int i) {
            var car = _cars[index];
            _cars.RemoveAt(index);
            _enemiesSettings.RemoveAt(index);
            SetCarToStack(car, i);
        }
        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if ((_playerPositionZ.value - _cars[i].position.z >= _playerLength.value / 2 + _enemiesLength[_enemiesSettings[i]].value / 2) && (_dodged)) {
                    Dodge(_enemiesScores[_enemiesSettings[i]]);
                    _currentScore.value += _enemiesScores[_enemiesSettings[i]].dodgeScore;
                    _dodged = false;
                }
                if (_playerPositionZ.value - _cars[i].position.z > _distanceToPlayerToDestroy) {
                    MoveCarToStack(i, _enemiesSettings[i]);
                }
            }
        }
        private void OnCarCollision() {
            UnsubscribeToEvents();
        }
    }
}
