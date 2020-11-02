using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game {
    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        private GameObject _carPrefab;

        [SerializeField]
        private float _spawnCooldown;

        [SerializeField]
        private float _distanceToPlayer;

        [SerializeField]
        private float _distanceDestroy;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue  _roadWidth;

        private float _currentTimer;


        private void OnEnable() {
            SuscribeToEvents();
        }
        private void OnDisable() {
            UnSuscribeToEvents();
        }

        private void SuscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionListener.OnEventHappened += OnCarCollision;
        }
        private void UnSuscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionListener.OnEventHappened -= OnCarCollision;
        }
        private void OnCarCollision() {
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
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value+_distanceToPlayer) ;
            var car = Instantiate(_carPrefab, position, Quaternion.Euler(0f, 180f, 0f));
            

        }

        private void HandleCarsBehindPlayer() {

        }

        

    }
}