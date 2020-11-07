using Events;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        
        public EnemyCarToDodge _dodgedCar;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        private EventDispatcher _carDodgeDispatcher;

        [SerializeField]
        private List<GameObject> _carPrefabs = new List<GameObject>();

        [SerializeField]
        private float _spawnCooldown;

        [SerializeField]
        private float _distanceToPlayerToSpawn;

        [SerializeField]
        private float _distanceToPlayerToDestroy;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue _playerPositionX;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        private float _currentTimer = 0f;
        private List<GameObject> _cars = new List<GameObject>();

        private void OnEnable() {
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnSubscribeToEvents();
        }

        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionListener.OnEventHappened += OnCarCollision;
        }
        private void UnSubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionListener.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {
            UnSubscribeToEvents();
        }

        private void UpdateBehaviour() {
            DodgeCheck();
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
            var enemyCarIndex = Random.Range(0, 3);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var car = Instantiate(_carPrefabs[enemyCarIndex], position, Quaternion.Euler(0f, 180f, 0f));
            _cars.Add(car);
            _dodgedCar.currentCar = car.gameObject;
        }

        private void DodgeCheck() {
            if(_dodgedCar.currentCar == null) {
                return;
            }
            if(Mathf.Abs(_dodgedCar.currentCar.transform.position.z - _playerPositionZ.value) < 10f && Mathf.Abs(_dodgedCar.currentCar.transform.position.x - _playerPositionX.value) < 3f) {
                _carDodgeDispatcher.Dispatch();
            }
        }

        private void HandleCarsBehindPlayer() {
            for(int i = _cars.Count - 1; i > -1; i--) {
                if(_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    Destroy(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }
    }
}

