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
        private GameObject _starPrefab;

        [SerializeField]
        private float _starDistanse;

        [SerializeField]
        private EventListener _starCollection;

        private float _currentTimer;
        private List<Car> _cars = new List<Car>();
        private GameObject _star;

        private Dictionary<string, SimpleGenericPool<Car>> _carPools; 

        private void Awake() {
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
            _starCollection.OnEventHappened += StarDestroy;
        }

        private void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionListener.OnEventHappened -= OnCarCollision;
            _starCollection.OnEventHappened -= StarDestroy;
        }

        private void StarDestroy() {
            Destroy(_star);
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
            var randomDerection = Random.Range(1, 3) * 2 - 3;
            var tmp = new Vector3(0f, 0f, _starDistanse * randomDerection);
            var positionStar = position + tmp;
            if (_star != null) {
                Destroy(_star);
            }
            _star = Instantiate(_starPrefab, positionStar, Quaternion.Euler(0f, 0f, 0f));
            var car = _carPools[_carPrefabs[randomCarInd].name].Pop();
            car.transform.position = position;
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
            if ((_star != null)&& (_playerPositionZ.value - _star.transform.position.z > _distanceToPlayerToDestroy)) {
                Destroy(_star);
            }    
        }
    }
}