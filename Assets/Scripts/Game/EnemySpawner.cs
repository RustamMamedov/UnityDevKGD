using System;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;
using static UI.SettingsScreen;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField] private EventListener _updateEventListener;

        [SerializeField] private EventListener _carCollisionListener;

        [SerializeField]
        [ValidateInput(nameof(ValidateListItems))]
        private List<GameObject> _carPrefabs;

        [SerializeField] private float _spawnCooldown;

        [SerializeField] private float _spawnCooldownEasy;

        [SerializeField] private float _spawnCooldownHard;


        [SerializeField] private float _distanceToPlayerToSpawn;

        [SerializeField] private float _distanceToPlayerDestroy;

        [SerializeField] private ScriptableFloatValue _playerPositionZ;

        [SerializeField] private ScriptableFloatValue _roadWidth;

        private List<GameObject> _cars = new List<GameObject>();

        private float _currentTimer = 0f;

        private const string RECORDS_KEY = "settings";


        private Dictionary<string, Stack<GameObject>> _carPool = new Dictionary<string, Stack<GameObject>>();

        public GameObject GetGameObjectFromPool(GameObject car) {
            if (!_carPool.ContainsKey(car.name)) {
                _carPool[car.name] = new Stack<GameObject>();
            }

            GameObject spawnCar;

            if (_carPool[car.name].Count == 0) {
                _carPool[car.name].Push(CreateCar(car));
            }
            spawnCar = _carPool[car.name].Pop();
            spawnCar.name = car.name;
            spawnCar.SetActive(true);
            return spawnCar;
        }

        private GameObject CreateCar(GameObject car) {
            var enemyCar = Instantiate(car, Vector3.zero, Quaternion.Euler(0f, 180f, 0f));
            enemyCar.SetActive(false);
            return enemyCar;
        }

        public void PutGameObjectToPool(GameObject car) {
            _carPool[car.name].Push(car);
            car.SetActive(false);
        }


        [Serializable]
        private class SavedDataWrapper {
            public SaveData savedData;
        }
        private static SaveData _saveData;


        private void Awake() {
            LoadFromPlayerPrefs();
            if (_saveData.difficult == 1) {
                _spawnCooldown = _spawnCooldownEasy;
            } else {
                _spawnCooldown = _spawnCooldownHard;
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
            HandleCarsBehindPlayers();
            _currentTimer += Time.deltaTime;
            if (_currentTimer < _spawnCooldown) {
                return;
            }

            _currentTimer = 0f;

            SpawnCar();
        }

        private void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            var randomCar = Random.Range(0, _carPrefabs.Count);
            var position = new Vector3(randomRoad * 1f * _roadWidth.value, 0,
                _playerPositionZ.value + _distanceToPlayerToSpawn);
            var car = GetGameObjectFromPool(_carPrefabs[randomCar]);
            car.transform.position = position;
            _cars.Add(car);
        }

        private void HandleCarsBehindPlayers() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerDestroy) {
                    PutGameObjectToPool(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }

        private bool ValidateListItems() {
            for (int i = 0; i < _carPrefabs.Count - 1; i++) {
                for (int j = i + 1; j < _carPrefabs.Count; j++) {
                    if (_carPrefabs[i].Equals(_carPrefabs[j])) {
                        return false;
                    }
                }
            }

            return true;
        }

        private void LoadFromPlayerPrefs() {
            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _saveData = wrapper.savedData;

        }

    }
}