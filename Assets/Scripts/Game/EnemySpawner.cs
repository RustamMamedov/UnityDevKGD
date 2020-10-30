using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private EventListener _carCollisionEventListener;

        [SerializeField]
        private List<GameObject> _carPrefab=new List<GameObject>();

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
        private ScriptableFloatValue _playerLength;

        [SerializeField]
        private List<ScriptableFloatValue> _enemiesLength=new List<ScriptableFloatValue>();

        [SerializeField]
        private List<CarSettings> _enemiesScores=new List<CarSettings>();

        [SerializeField]
        private ScriptableIntValue _currentScore;

        private List<int> _enemiesSettings=new List<int>();
        private int _numberOfPrefab; 
        private float _currentTimer;
        private bool _dodged;
        private List<GameObject> _cars = new List<GameObject>();
        private void OnEnable()
        {
            SubscribeToEvents();
        }
        private void OnDisable()
        {
            UnsubscribeToEvents();
        }
        private void SubscribeToEvents()
        {
            _updateEventListener.OnEventHappened += UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened += OnCarCollision;
        }
        private void UnsubscribeToEvents()
        {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _carCollisionEventListener.OnEventHappened -= OnCarCollision;
        }
        private void UpdateBehaviour()
        {
            HandleCarsBehindPlayer();
            _currentTimer += Time.deltaTime;
            if (_currentTimer < _spawnCooldown)
            {
                return;
            }
            _currentTimer = 0f;
            SpawnCar();
        }
        private void SpawnCar()
        {
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);
            _numberOfPrefab = Random.Range(0, 3);
            var car = Instantiate(_carPrefab[_numberOfPrefab],position,Quaternion.Euler(0f,180f,0f));
            _cars.Add(car);
            _dodged = true;
            _enemiesSettings.Add(_numberOfPrefab);
        }
        private void HandleCarsBehindPlayer()
        {
            for (int i = _cars.Count - 1; i > -1; i--)
            {
                if ((Mathf.Abs(_playerPositionZ.value - _playerLength.value / 2 - _cars[i].transform.position.z + _enemiesLength[_enemiesSettings[i]].value / 2) < 1f)&&(_dodged))
                {
                    _currentScore.value += _enemiesScores[_enemiesSettings[i]].dodgeScore;
                    _dodged = false;
                }
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy)
                {
                    Destroy(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }
        private void OnCarCollision()
        {
            UnsubscribeToEvents();
        }
    }
}
