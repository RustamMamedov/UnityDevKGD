using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Sirenix.OdinInspector;

namespace Game {

    public class EnemySpawner : MonoBehaviour {

	[SerializeField]
	private EventListener _updateEventListener;

	[SerializeField]
	private EventListener _carCollisionListener;

	[Header ("Вражины в игре: ")]
	[SerializeField]
	[ValidateInput(nameof(ValidateEnemyCars))]
	private List<GameObject> _carPrefab = new List<GameObject>();

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
	private List<GameObject> _cars = new List<GameObject>();


	private bool ValidateEnemyCars() {
	    bool isTrue = true;
	    for (int i = 0; i < _carPrefab.Count - 1; i++) {
		if (_carPrefab[i] == _carPrefab[_carPrefab.Count - 1]) {
		    isTrue = false;
		    break;
		}
	    }
	    return isTrue;
	}

	private void OnEnable() {
	    SubscribeToEvents();
	}

	private void OnDisable() {
	    UnsubscribeToEvents();
	}

	private void SubscribeToEvents() {
	    _updateEventListener.OnEventHappend += UpdateBehaviour;
	    _carCollisionListener.OnEventHappend += OnCarCollision;
	}

	private void UnsubscribeToEvents() {
	    _updateEventListener.OnEventHappend -= UpdateBehaviour;
	    _carCollisionListener.OnEventHappend -= OnCarCollision;
	}

	private void OnCarCollision() {
	    UnsubscribeToEvents();
	}

	private void UpdateBehaviour() {
	    HandlerCarsBehindPlayer();

	    _currentTimer += Time.deltaTime;
	    if (_currentTimer < _spawnCooldown) {
		return;
	    }

	    _currentTimer = 0;

	    SpawnCar();
	}

	private void SpawnCar() {
	    var randomRoad = Random.Range(-1, 2);
	    var position = new Vector3(1f * randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn);

	    var randomCar = Random.Range(0, 3);
	    var car = Instantiate(_carPrefab[randomCar], position, Quaternion.Euler(0f, 180f, 0f));
	    _cars.Add(car);
	}

	private void HandlerCarsBehindPlayer() {
	    for (int i = _cars.Count - 1; i > -1; i--) {
		if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
		    Destroy(_cars[i]);
		    _cars.RemoveAt(i);	  
		}
	    }
	}
    }
}
