using UI;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Random = UnityEngine.Random;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        private ScriptableIntValue _currentScore;

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

        private float _currentTimer;
        private List<GameObject> _cars;

        private void OnEnable() {

            _cars = new List<GameObject>();
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

            _currentTimer += Time.deltaTime;
            if (_currentTimer < _spawnCooldown) {

                return;
            }

            _currentTimer = 0f;
            SpawnCar();
            HandleCarsBehindPlayer();
        }

        private void SpawnCar() {

            var randomRoad = Random.Range(-1,2);
            var carIndex = Random.Range(0,3);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var car = Instantiate(_carPrefabs[carIndex], position, Quaternion.Euler(0f, 180f, 0f));
            _cars.Add(car);
        }

        private void HandleCarsBehindPlayer() {

            for (int i = _cars.Count - 1; i > - 1; i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    _currentScore.value += _cars[i].gameObject.GetComponent<EnemyCar>().CarSettings.dodgeScore;
                    Destroy(_cars[i]); 
                    _cars.RemoveAt(i);
                    
                }
            }
        }
    }
}

