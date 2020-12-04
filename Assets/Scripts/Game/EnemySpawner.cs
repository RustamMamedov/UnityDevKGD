using System.Collections.Generic;
using Events;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        [ValidateInput(nameof(CarPrefabsValidate))]
        private List<GameObject> _carPrefab;

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
        private ScriptableIntValue _diffCurrent;

        private float _currentTimer;

        private List<GameObject> _cars;
        private Dictionary<int, Stack<GameObject>> _carsPool;

        private void OnEnable() {
            SubscribeToEvents();
            if (_diffCurrent.value == 1) {
                _spawnCooldown = 2f;
            }
            else {
                _spawnCooldown = 5f;
            }
            GeneratePool();
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
            var randomEnemy = Random.Range(0, _carPrefab.Count);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var car = GetCarFromPool(_carPrefab[randomEnemy], position);
            _cars.Add(car);
        }

        public GameObject GetCarFromPool(GameObject carPrefab, Vector3 position) {

            GameObject enemyCar;

            if (_carsPool[carPrefab.GetComponent<EnemyCar>().CarSettings.id].Count > 0) {
                enemyCar = _carsPool[carPrefab.GetComponent<EnemyCar>().CarSettings.id].Pop();
                enemyCar.transform.position = position;
                enemyCar.SetActive(true);
                enemyCar.GetComponent<EnemyCar>().SetActive();
                return enemyCar;
            }

            enemyCar = Instantiate(carPrefab, position, Quaternion.Euler(0f, 180f, 0f));
            return enemyCar;
        }


        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    PutCarToPool(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }

        public void PutCarToPool(GameObject enemyCar) {
            _carsPool[enemyCar.GetComponent<EnemyCar>().CarSettings.id].Push(enemyCar);
            enemyCar.SetActive(false);
        }

        private bool CarPrefabsValidate() {

            // _carPrefab.Distinct() - returns the number of distinct elements in List
            if (_carPrefab.Count == _carPrefab.Distinct().Count()) {
                return true;
            }
            return false;
        }

        private void GeneratePool() {
            _carsPool = new Dictionary<int, Stack<GameObject>>();
            _cars = new List<GameObject>();

            foreach (GameObject enemyCar in _carPrefab) {
                _carsPool[enemyCar.GetComponent<EnemyCar>().CarSettings.id] = new Stack<GameObject>();
            }
        }
    }
}