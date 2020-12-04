using System.Collections.Generic;
using Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [ValidateInput(nameof(ValidateEnemyCars))]
        [SerializeField]
        private List<GameObject> _enemyCars = new List<GameObject>();

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

        private float _currentTimer;
        private List<GameObject> _cars = new List<GameObject>();
        private Dictionary<int, Stack<GameObject>> _enemyCarsPool;
        private float _easySpawnCooldown = 4f;
        private float _hardSpawnCooldown = 1f;

        private void Awake() {
            if (PlayerPrefs.GetInt("Difficulty").Equals(0)) {
                _spawnCooldown = _easySpawnCooldown;
            } else {
                _spawnCooldown = _hardSpawnCooldown;
            }
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

            _currentTimer += Time.deltaTime;
            if (_currentTimer < _spawnCooldown) {
                return;
            }
            _currentTimer = 0f;

            SpawnCar();
        }

        private void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var car = GetCarFromStack(_enemyCars[Random.Range(0, _enemyCars.Count)], position);
            _cars.Add(car);
        }

        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    PutCarToStack(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }

        private bool ValidateEnemyCars() {
            bool isTrue = true;
            for (int i = 0; i < _enemyCars.Count-1; i++) {
                if (_enemyCars[i] == _enemyCars[_enemyCars.Count-1]) {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        private void GeneratePool() {
            _enemyCarsPool = new Dictionary<int, Stack<GameObject>>();
            foreach (GameObject enemyCar in _enemyCars) {
                _enemyCarsPool[enemyCar.GetComponent<EnemyCar>().CarSettings.dodgeCarId] = new Stack<GameObject>();
            }
        }

        public void PutCarToStack(GameObject enemyCarToStack) {
            _enemyCarsPool[enemyCarToStack.GetComponent<EnemyCar>().CarSettings.dodgeCarId].Push(enemyCarToStack);
            enemyCarToStack.SetActive(false);
        }

        public GameObject GetCarFromStack(GameObject enemyCarPrefab, Vector3 position) {
            GameObject enemyCarToSpawn;
            if (_enemyCarsPool[enemyCarPrefab.GetComponent<EnemyCar>().CarSettings.dodgeCarId].Count == 0) {
                enemyCarToSpawn = Instantiate(enemyCarPrefab, position, Quaternion.Euler(0f, 180f, 0f));
                return enemyCarToSpawn;
            } else {
                enemyCarToSpawn = _enemyCarsPool[enemyCarPrefab.GetComponent<EnemyCar>().CarSettings.dodgeCarId].Pop();
                enemyCarToSpawn.transform.position = position;
                enemyCarToSpawn.SetActive(true);
                return enemyCarToSpawn;
            }
        }
    }
}