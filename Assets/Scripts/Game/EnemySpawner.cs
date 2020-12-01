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

        [ValidateInput(nameof(ValidationListEnemyCars))]

        [SerializeField]
        private List<GameObject> _carPrefabs;

        [SerializeField]
        private ScriptableIntValue _difficultyGame;

        private bool ValidationListEnemyCars(List<GameObject> carPrefabs) {
            for (int i = 0; i < carPrefabs.Count; i++) {
                if (i != carPrefabs.LastIndexOf(carPrefabs[i])) {
                    return false;
                }
            }
            return true;
        }

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

        private bool _spawn = false;

        private int canEmptyRoad = -2;

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
            if (!_spawn)
            for (int i = 0; i < GetDifficultyGame(); i++) {
                _spawn = true;
                SpawnCar();
            }
            _spawn = false;
            _currentTime = 0;
        }

        private int GetDifficultyGame() {
            if (_difficultyGame.Value == 0) {
                _spawnCooldawn = 3;
                return 1;
            }
            _spawnCooldawn = 1;
            return 2;
        }

        private void SpawnCar() {
            var randomCar = _carPrefabs[Random.Range(0, 3)];
            var randomRoad = Random.Range(-1, 2);
            while (canEmptyRoad == randomRoad) {
                randomRoad = Random.Range(-1, 2);
            }
            canEmptyRoad = randomRoad;
            var position = new Vector3(1f * randomRoad * _roadWidth.Value, 0f, _playerDistanseZ.Value + _distanceToPlaySpawn);
            var car = Instantiate(randomCar, position, Quaternion.Euler(0f, 180f, 0f));
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
