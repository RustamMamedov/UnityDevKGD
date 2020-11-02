using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using UI;
using UnityEngine.UI;

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
        private float _distanceToPlayerToSpawn;

        [SerializeField]
        private float _distanceToPlayerToDestroy;

        [SerializeField]
        private ScriptableFloatValue _playerPositionZ;

        [SerializeField]
        private ScriptableFloatValue _roadWidth;

        private bool _scoreCountAllow = true;

        private bool _onSpawnCar;

        private float _currentTimer;

        private List<GameObject> _cars = new List<GameObject>();

        private int _dodgeScore = 0;

        [SerializeField]
        private List<GameObject> _carsToSpawn = new List<GameObject>();

        private void OnEnable() {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionListener.OnEventHappened += OnCarCollision;
        }

        private void OnDisable() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionListener.OnEventHappened -= OnCarCollision;
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
            if (_currentTimer < _spawnCooldown) {

                if (_onSpawnCar) {
                    var distanceToSpawnedCar = _cars[0].transform.position.z - _playerPositionZ.value;

                    if (distanceToSpawnedCar < 5 && _scoreCountAllow) {
                        var scoreLabel = UIManager.Instance.GameScreen.transform.GetChild(0).GetChild(0).GetComponent<Text>();
                    
                        _dodgeScore += _cars[0].GetComponent<EnemyCar>()._carSettings.dodgeScore; ;
                        scoreLabel.text = _dodgeScore.ToString();
                        _scoreCountAllow = false;
                    }

                }
                return;
            }
            _currentTimer = 0;
            SpawnCar();
        }
        private void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);

            var randomCarIndex = Random.Range(0, _carsToSpawn.Count);
            var car = Instantiate(_carsToSpawn[randomCarIndex], position, Quaternion.Euler(0, 180, 0));

            _cars.Add(car);
            _onSpawnCar = true;
        }

        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    Destroy(_cars[i]);
                    _cars.Remove(_cars[i]);
                    _scoreCountAllow = true;
                    _onSpawnCar = false;
                }
            }
        }
    }

}
