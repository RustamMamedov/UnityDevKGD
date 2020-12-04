using UnityEngine;
using Events;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Game {
    
    public class EmenySpawner : MonoBehaviour {
        
        [SerializeField]
        private EventListener _updateEventListener;
        
        [SerializeField]
        private EventListener _carCollisionListener;

        [SerializeField]
        private ScriptableIntValue _gameMode;


        [ValidateInput(nameof(VolidateList))]
        [SerializeField]
        private List<GameObject> _carsPrefab = new List<GameObject>();
        
        private bool VolidateList() {
            for(int i = 0; i < _carsPrefab.Count-1; i++) {
                for (int j = i+1; j < _carsPrefab.Count; j++) {
                    if (_carsPrefab[i] == _carsPrefab[j])
                        return false;
                }
            }
            return true;
        }

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
        
        //private List<GameObject> _cars=new List<GameObject>();

        private List<Stack<GameObject>> _carStacks;

        [SerializeField]
        [ValidateInput(nameof(VolidateStartCount))]
        private int _startedCountStack;

        private bool VolidateStartCount() {
            if (_startedCountStack > 0)
                return true;
            else
                return false;
        }

        private struct CarAndType {

            public CarAndType(GameObject c, int t) {
                car = c;
                type = t;
            }

            public GameObject car;
            public int type;
        }

        private List<CarAndType> _cars=new List<CarAndType>();

        #region ListenerAdded and OnEnable/OnDisable

        private void OnEnable() {
            SubscribeToEvents();
        }

        private void OnDisable() {
            UnsubscribeToEvents();
        }

        private void OnCarCollision() {
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

        #endregion ListenerAdded and OnEnable/OnDisable

        private void Awake() {
            _carStacks = new List<Stack<GameObject>>();
            for (int i = 0; i < _carsPrefab.Count; i++) {
                Stack<GameObject> empty=new Stack<GameObject>();   
                _carStacks.Add(empty);
                for (int j = 0; j < _startedCountStack; j++) {
                    Debug.Log(_carsPrefab[i]);
                    PutInStack(CreateCar(_carsPrefab[i]),i);
                }
            }
        }

        private GameObject CreateCar(GameObject prefab) {
            return Instantiate(prefab,Vector3.zero,Quaternion.Euler(0f, 180f, 0f));
        }

        #region StackCar

        private GameObject GetFromStack(int currentStack) {
            //int _currentStack=Random.Range(0, _carStacks.Count);
            if (_carStacks[currentStack].Count > 0) {
                var car = _carStacks[currentStack].Pop();
                car.SetActive(true);
                return car;
            }
            else
                return CreateCar(_carsPrefab[currentStack]);
        }

        private void  PutInStack(GameObject car, int currentStack) {
            car.SetActive(false);
            _carStacks[currentStack].Push(car);
        }

        #endregion StackCar

        private void UpdateBehaviour() {
            HandleCarsBehindPlayer();
            _currentTimer += Time.deltaTime;
            if (_currentTimer<_spawnCooldown) {
                return;
            }
            _currentTimer = 0f;

            SpawnCar();
        }

        private void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3((float)randomRoad * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn + Random.Range(-_distanceToPlayerToSpawn * 0.4f, _distanceToPlayerToSpawn * 0.4f));
            int carStack1 = Random.Range(0, _carStacks.Count);
            var car = GetFromStack(carStack1);
            car.transform.position = position;
            _cars.Add(new CarAndType(car, carStack1));
            if ((_gameMode.value == 1)/*&& (Random.Range(0, 2) == 0)*/) {
                var rand = Random.Range(1, 3);
                int randomRoad2= randomRoad == 0? rand * 2 - 3: randomRoad * (1 - rand);
                var position2 = new Vector3((float)randomRoad2 * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn + Random.Range(-_distanceToPlayerToSpawn * 0.4f, _distanceToPlayerToSpawn * 0.4f));
                int carStack2 = Random.Range(0, _carStacks.Count);
                var car2= GetFromStack(carStack1);
                car2.transform.position = position2;
                _cars.Add(new CarAndType(car2, carStack2));
            }
        }

        private void HandleCarsBehindPlayer() {
            for (int i = _cars.Count - 1; i > -1; i--) {
                if (_playerPositionZ.value - _cars[i].car.transform.position.z > _distanceToPlayerToDestroy) {
                    PutInStack(_cars[i].car, _cars[i].type);
                    _cars.RemoveAt(i);
                }
            }
        }

        /*
        private void SpawnCar() {
            var randomRoad = Random.Range(-1, 2);
            var position = new Vector3((float)randomRoad*_roadWidth.value,0f,_playerPositionZ.value+_distanceToPlayerToSpawn+Random.Range(-_distanceToPlayerToSpawn*0.4f, _distanceToPlayerToSpawn * 0.4f));
            //var car = Instantiate(_carsPrefab[Random.Range(0, _carsPrefab.Count)], position,Quaternion.Euler(0f,180f,0f));
            var car = CreateCar(_carsPrefab[Random.Range(0, _carsPrefab.Count)]);
            car.transform.position = position;
            _cars.Add(car);
            if (_gameMode.value == 1) {
                if (Random.Range(0, 2) % 2 == 0) {
                    int randomRoad2;
                    do {
                        randomRoad2 = Random.Range(-1, 2);
                    } while (randomRoad == randomRoad2);
                    var position2 = new Vector3((float)randomRoad2 * _roadWidth.value, 0f, _playerPositionZ.value + _distanceToPlayerToSpawn + Random.Range(-_distanceToPlayerToSpawn * 0.4f, _distanceToPlayerToSpawn * 0.4f));
                    //var car2 = Instantiate(_carsPrefab[Random.Range(0, _carsPrefab.Count)], position2, Quaternion.Euler(0f, 180f, 0f));
                    var car2 = CreateCar(_carsPrefab[Random.Range(0, _carsPrefab.Count)]);
                    car.transform.position = position2;
                    _cars.Add(car2);
                }
            }
        }

        private void HandleCarsBehindPlayer() {
            for (int i= _cars.Count-1; i >-1;i--) {
                if (_playerPositionZ.value - _cars[i].transform.position.z > _distanceToPlayerToDestroy) {
                    Destroy(_cars[i]);
                    _cars.RemoveAt(i);
                }
            }
        }
        */
    }
}