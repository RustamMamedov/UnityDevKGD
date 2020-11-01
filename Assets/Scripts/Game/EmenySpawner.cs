using UnityEngine;
using Events;
using System.Collections.Generic;

namespace Game {
    public class EmenySpawner : MonoBehaviour {
        [SerializeField]
        private EventListener _updateEventListener;
        [SerializeField]
        private EventListener _carCollisionListener;
        [SerializeField]
        private List<GameObject> _carsPrefab = new List<GameObject>();
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
        private List<GameObject> _cars=new List<GameObject>();

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

        private void UpdateBehaviour() {
            HandleCarsBehindPlayer();
            _currentTimer += Time.deltaTime;
            if (_currentTimer<_spawnCooldown) {
                return;
            }
            _currentTimer = 0f;

            SpawnCar();
        }
        
        private void OnCarCollision() {
            UnsubscribeToEvents();
        }

        private void SpawnCar() {
            
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3((float)randomRoad*_roadWidth.value,0f,_playerPositionZ.value+_distanceToPlayerToSpawn+Random.Range(-_distanceToPlayerToSpawn*0.4f, _distanceToPlayerToSpawn * 0.4f));
            var car = Instantiate(_carsPrefab[Random.Range(0, _carsPrefab.Count)], position,Quaternion.Euler(0f,180f,0f));
            _cars.Add(car);
            //это не для задания но прикольно когда так)
            /*
            if (Random.Range(0, 2) % 2 == 0) {
                int randomRoad2;
                do {
                    randomRoad2 = Random.Range(-1, 2);
                } while (randomRoad == randomRoad2);
                var position2 = new Vector3((float)randomRoad2 * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn + Random.Range(-_distanceToPlayerToSpawn * 0.4f, _distanceToPlayerToSpawn * 0.4f));
                var car2 = Instantiate(_carsPrefab[Random.Range(0, _carsPrefab.Count)], position2, Quaternion.Euler(0f, 180f, 0f));
                _cars.Add(car2);
            }*/
        }

        private void HandleCarsBehindPlayer() {
            for (int i= _cars.Count-1; i >-1;i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    Destroy(_cars[i]);
                    _cars.RemoveAt(i);
                    
                }
            }
        }
    }
}