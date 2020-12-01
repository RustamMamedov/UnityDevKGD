using System.Collections.Generic;
using Events;
using UnityEngine;
using Sirenix.OdinInspector;
using UI;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [ValidateInput(nameof(ValidateCarPrefabsList))]
        [SerializeField]
        private List<GameObject> _carPrefabs;

        [SerializeField]
        private float _spawnCooldown;

        [SerializeField]
        private float _hardSpawnCooldown;

        [SerializeField]
        private float _distanceToPlayerToSpawn;

        [SerializeField]
        private float _distanceToPlayerToDestroy;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        [SerializeField]
        private int _listOfEnemyCarsSize; 

        private float _currentTimer;
        [SerializeField]

        private List<GameObject> _cars = new List<GameObject>();
        [SerializeField]
        private List<GameObject> _listOfEnemyCars;

        private void Awake() {
            if(Settings.Instance!=null) {
                if(!Settings.Instance.IsEasy) {
                    _spawnCooldown =_hardSpawnCooldown;
                }
            }
            
        }

        private void Start() {
            GeneratePool();
        }
        

        void GeneratePool() {
            _listOfEnemyCars = new List<GameObject>();
            for(int i = 0;  i<_listOfEnemyCarsSize / _carPrefabs.Count;i++) {
                for(int j = 0;j < _carPrefabs.Count;j++) {
                    _listOfEnemyCars.Add(Instantiate(_carPrefabs[j]));
                    _listOfEnemyCars[_listOfEnemyCars.Count - 1].SetActive(false);
                }
            }
            for(int i =0;i<_listOfEnemyCarsSize % _carPrefabs.Count;i++) {
                _listOfEnemyCars.Add(Instantiate(_carPrefabs[i]));
                _listOfEnemyCars[_listOfEnemyCars.Count - 1].SetActive(false);
            }
        }

        private bool ValidateCarPrefabsList(List<GameObject> carPrefabs) {
            for (int i = 0; i < carPrefabs.Count - 1; i++) {
                for (int j = i+1; j < carPrefabs.Count; j++) {
                    if(carPrefabs[i]==carPrefabs[j]) {
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

            if(_listOfEnemyCars.Count==0) {
                var randomCar = Instantiate(_carPrefabs[Random.Range(0,_carPrefabs.Count)]);
                randomCar.SetActive(false);
                _listOfEnemyCars.Add(randomCar);
            }

            var randomCarNum = Random.Range(0, _listOfEnemyCars.Count);

            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            _listOfEnemyCars[randomCarNum].transform.position = position;
            _listOfEnemyCars[randomCarNum].transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _listOfEnemyCars[randomCarNum].SetActive(true);
            _cars.Add(_listOfEnemyCars[randomCarNum]);
            _listOfEnemyCars.RemoveAt(randomCarNum);
        }

        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    _cars[i].SetActive(false);
                    _listOfEnemyCars.Add(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }
    }
}