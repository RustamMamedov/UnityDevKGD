using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
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
        private ScriptableIntValue _difficult;

        [SerializeField]
        private int initialStackCarNumber;

        private float _currentTimer;
        private List<GameObject> _cars = new List<GameObject>();

        private Queue<GameObject> _queue;


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
            if (_difficult.value == 0) {
                _currentTimer += Time.deltaTime;
            }
            else {
                _currentTimer += 2 * Time.deltaTime;
            }

            if (_currentTimer < _spawnCooldown) {
                return;
            }
            _currentTimer = 0f;

            SpawnCar();
        }


        private GameObject CreateCar() {
            var car = Instantiate(_carPrefabs[Random.Range(0, _carPrefabs.Count)], Vector3.zero, Quaternion.Euler(0f, 180f, 0f));
            car.SetActive(false);
            return car;

        }

        private void GeneratePool() {
            _queue = new Queue<GameObject>();
            for (int i = 0; i < initialStackCarNumber; i++) {
                _queue.Enqueue(CreateCar());

            }

        }

        private GameObject GetCarFromStack() {
            if(_queue.Count == 0) {
                _queue.Enqueue(CreateCar());
            }

            var car = _queue.Dequeue();
            car.SetActive(true);
            return car;
        }

        private void SetCarToStack(GameObject car) {
            car.SetActive(false);
            _queue.Enqueue(car);
        }

        private void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var car = GetCarFromStack();
            car.transform.position = position;
            _cars.Add(car);
        }

        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    SetCarToStack(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }
    }
}