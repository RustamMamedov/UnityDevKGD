using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListeners _updateEventListener;

        [SerializeField]
        private EventListeners _carCollisionEventLister;

        [SerializeField]
        private GameObject _carPrefab;

        [SerializeField]
        private float _spawnCooldawn;

        [SerializeField]
        private float _distanceToPlaySpawn;

        [SerializeField]
        private float _distanceToPlayDestrou;

        [SerializeField]
        private ScriptableFloatValue _playerDistanseZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        private List<GameObject> _cars = new List<GameObject>();

        private float _currentTime;

        private void OnEnable() {
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents() {
            _updateEventListener.OnEventHappened += UpdateBehavour;
            _carCollisionEventLister.OnEventHappened += OnCarCollision;
        }

        private void UnsubscribeToEvents() {
            _updateEventListener.OnEventHappened -= UpdateBehavour;
            _carCollisionEventLister.OnEventHappened -= OnCarCollision;
        }

        private void OnCarCollision() {
            UnsubscribeToEvents();
        }

        private void UpdateBehavour() {
            HendleCarBehaindPlayer();

            _currentTime += Time.deltaTime;
            if (_currentTime < _spawnCooldawn) {
                return;
            }
            _currentTime = 0;
            SpawnCar();
        }

        private void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3(1f * randomRoad* _roadWidth.Value, 0f, _playerDistanseZ.Value+_distanceToPlaySpawn);
            var car = Instantiate(_carPrefab, position, Quaternion.Euler(0f, 180f, 0f));
            _cars.Add(car);
        }

        private void HendleCarBehaindPlayer() {
            for (int i= _cars.Count-1; i>-1;i--) {
                if (_playerDistanseZ.Value - _cars[i].transform.position.z > _distanceToPlayDestrou) {
                    Destroy(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }
    }
}
