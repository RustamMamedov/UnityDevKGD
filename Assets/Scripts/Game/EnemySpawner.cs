using UnityEngine;
using Events;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        [ValidateInput(nameof(ValidateCarPrefs), "Was added two equal car prefabs")]
        private List<GameObject> _carPrefabs = new List<GameObject>();

        [SerializeField]
        private List<CarSettings> _carSettings = new List<CarSettings>();

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
        private ScriptableIntValue _currentScore;

        [SerializeField]
        private ScriptableIntValue[] _dodgeScores;

        private float _currentTimer;

        private List<GameObject> _cars = new List<GameObject>();

        private List<int> _carsIndexs = new List<int>();

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
            if (_currentTimer < _spawnCooldown) {
                return;
            }
            _currentTimer = 0f;

            SpawnCar();
        }

        private void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            int randomEnemy = Random.Range(0, 3);

            _carsIndexs.Add(randomEnemy);

            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            var car = Instantiate(_carPrefabs[randomEnemy], position, Quaternion.Euler(0f, 180f, 0f));
            _cars.Add(car);
        }

        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    _currentScore.value += _carSettings[_carsIndexs[i]].dodgeScore;
                    _dodgeScores[_carsIndexs[i]].value++;
                    Destroy(_cars[i]);
                    _cars.RemoveAt(i);
                    _carsIndexs.RemoveAt(i);
                }
            }
        }

        private bool ValidateCarPrefs(List<GameObject> prefs) {

            foreach (var item in prefs) {
                List<GameObject> tempList = prefs.FindAll(
                    delegate (GameObject it) {
                        return it.name == item.name;
                    });
                if (tempList.Count > 1) {
                    return false;
                }
            }
            return true;
        }

    }
}
