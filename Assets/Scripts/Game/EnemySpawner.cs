using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Events;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListeners _updateEventListener;

        [SerializeField]
        private EventListeners _carCollisionEventLister;

        [ValidateInput(nameof(ValidationCarPrefab))]

        [SerializeField]
        private List<GameObject> _carPrefabs;

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

        [SerializeField]
        private ScriptableIntValue _complexity;

        private List<GameObject> _cars = new List<GameObject>();

        private float _currentTime;

        private bool ValidationCarPrefab(List<GameObject> carPrefabs) {
            for (int i = 0; i < carPrefabs.Count; i++) {
                if (i != carPrefabs.LastIndexOf(carPrefabs[i])) {
                    return false;
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
            var randomCar = _carPrefabs[Random.Range(0, 3)];
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerDistanseZ.value + _distanceToPlaySpawn);
            var car = Instantiate(randomCar, position, Quaternion.Euler(0f, 180f, 0f));
            _cars.Add(car);
            if (_complexity.value == 1) {
                if (Random.Range(0, 2) % 2 == 0) {
                    int randomRoad2;
                    do {
                        randomRoad2 = Random.Range(-1, 2);
                    } while (randomRoad == randomRoad2);
                    var position2 = new Vector3((float)randomRoad2 * _roadWidth.value, 0f, _playerDistanseZ.value + _distanceToPlaySpawn + Random.Range(-_distanceToPlaySpawn * 0.4f, _distanceToPlaySpawn * 0.4f));
                    var car2 = Instantiate(_carPrefabs[Random.Range(0, _carPrefabs.Count)], position2, Quaternion.Euler(0f, 180f, 0f));
                    _cars.Add(car2);
                }
            }
        }

        private void HendleCarBehaindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerDistanseZ.value - _cars[i].transform.position.z > _distanceToPlayDestrou) {
                    Destroy(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }
    }
}
