using System.Collections.Generic;
using Events;
using UnityEngine;
using Utils;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        private List<Car> _carPrefabs;

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
        private ScriptableFloatValue _starSpawnDistanse;

        [SerializeField]
        private GameObject _starPrefab;

        [SerializeField]
        private int _initialStackStarNumber = 3;

        private Stack<GameObject> _starPool;

        

        private float _currentTimer;
        private List<Car> _cars = new List<Car>();

        private Dictionary<string, SimpleGenericPool<Car>> _carPools;

        private void Awake() {

            GeneratePool();

            _carPools = new Dictionary<string, SimpleGenericPool<Car>>();
            for (int i = 0; i < _carPrefabs.Count; i++) {
                _carPools[_carPrefabs[i].Name] = new SimpleGenericPool<Car>(_carPrefabs[i]);
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

            SpawnRandomCar();
        }

        private void SpawnRandomCar() {
            var randomRoad = Random.Range(-1, 2);
            var randomCarInd = Random.Range(0, 3);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var car = _carPools[_carPrefabs[randomCarInd].name].Pop();
            car.transform.position = position;
            SpawnStar(car.transform);
            car.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _cars.Add(car);
        }

        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    _carPools[_cars[i].Name].Push(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }

        private void SpawnStar(Transform _carTransform) {
            var star = GetStarFromStack();
            var multiply = Random.Range(0, 2);
            switch (multiply){
                case 0:
                    multiply = -1;
                    break;
                case 1:
                    multiply = 1;
                    break;
            }
            var _starPosition = new Vector3(_carTransform.position.x,_carTransform.position.y,_carTransform.position.z * multiply);
            star.transform.position = _starPosition;

        }

        private GameObject GetStarFromStack() {
            if (_starPool.Count == 0) {
                _starPool.Push(_starPrefab);
            }

            var star = _starPool.Pop();
            star.SetActive(true);
            return star;
        }

        private void GeneratePool() {
            _starPool = new Stack<GameObject>();
            for (int i = 0; i < _initialStackStarNumber; i++) {
                _starPool.Push(_starPrefab);
            }
        }

        private void SetStarToStack(GameObject star) {
            star.gameObject.SetActive(false);
            _starPool.Push(star);
        }
    }
}