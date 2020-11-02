using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;


namespace Game {
    public class EnemySpawner : MonoBehaviour {
        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        private List<GameObject> _carPrefabs = new List<GameObject>();

        [SerializeField]
        private float _spawnCooldown;

        [SerializeField]
        private float _dictanceToPlayerToSpawn;

        [SerializeField]
        private float _distanceToPlayerDestroy;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        private float _currentTimer;

        private List<GameObject> _cars= new List<GameObject>();

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
            if (_currentTimer< _spawnCooldown) {
                return;
            }
            _currentTimer = 0f;

            SpawnCar();
        }

        private void SpawnCar() {
            
            var randomRoad = Random.Range(-1, 2);
            var randomCar = Random.Range(0, 3);

            var position = new Vector3(1f*randomRoad* _roadWidth.value,0f,_playerPositionZ.value + _dictanceToPlayerToSpawn );
            var car = Instantiate(_carPrefabs[randomCar], position, Quaternion.Euler(0f,180f,0f));
            _cars.Add(car);

        }

        private void HandleCarsBehindPlayer() {

            for(int i = _cars.Count-1; i >-1; i--) {
                if(_playerPositionZ.value- _cars[i].transform.position.z> _distanceToPlayerDestroy) {
                    Destroy(_cars[i]);
                    _cars.RemoveAt(i);
                   
                }
            }

        }

       

    }
}
