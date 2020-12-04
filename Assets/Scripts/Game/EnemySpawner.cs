using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        [ValidateInput(nameof(ValidateCarPrefab))]
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
        private ScriptableFloatValue _valueDificulty;

        private float _currentTimer; 
        private List<GameObject> _cars = new List<GameObject>();

        private Dictionary<string, Stack<GameObject>> _dictionary = new Dictionary<string, Stack<GameObject>>();

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

            if (_valueDificulty.value == 0) { 
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

        private void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var randomCar = Random.Range(0,_carPrefab.Count);
            var car = GameObjectFromPool(_carPrefab[randomCar], position);
            _cars.Add(car);
        }

        private void HandleCarsBehindPlayer() {
            
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    GameObjectPool(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        } 

        public void GameObjectPool(GameObject car) { 
            _dictionary[car.name].Push(car); 
            car.SetActive(false); 
        } 

        private bool ValidateCarPrefab() {
            
            for (int i = 0; i < _carPrefab.Count - 1; i++) {
            
                for (int j = i + 1; j < _carPrefab.Count; j++) {
            
                    if(_carPrefab[i] == _carPrefab[j]) {
                        return false;
                    }
            
                }
            }

            return true;
        }

        public GameObject GameObjectFromPool(GameObject car, Vector3 spawnPos) { 
            if (!_dictionary.ContainsKey(car.name)) { 
                _dictionary[car.name] = new Stack<GameObject>();
            } 
 
            GameObject result; 
 
            if (_dictionary[car.name].Count > 0) { 
                result = _dictionary[car.name].Pop(); 
                result.transform.position = spawnPos; 
                result.SetActive(true); 
 
                return result; 
            } 
            result = Instantiate(car, spawnPos, Quaternion.Euler(0f, 180f, 0f)); 
            result.name = car.name; 
 
            return result; 
        } 
    }
}
