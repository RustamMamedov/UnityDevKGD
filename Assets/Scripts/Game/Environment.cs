using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Sirenix.OdinInspector;

namespace Game {
    public class Environment : MonoBehaviour {

        [SerializeField]
        private EventListener _roadCollisionEventListener;

        [SerializeField]
        [AssetsOnly] // модно указать только то, что находится в проекте, а не на сцене
        private GameObject _roadPrefab; 

        [SerializeField]
        private int _initialRoadNumber = 10;

        [SerializeField]
        private int _initialStackRoadNumber = 5;

        [SerializeField]
        private int _roadLength = 12;

        [SerializeField]
        private Light _light;

        [SerializeField]
        private ScriptableIntValue _illumination;

        private List<Transform> _roadTransform;

        private Stack<Transform> _stack;

        private void Start() {
            if (_illumination.value == 0) {
                _light.gameObject.SetActive(true);
            }
            else {
                _light.gameObject.SetActive(false);
            }
            GeneratePool();
            GenerateRoad();
        }
        protected void OnEnable() {
            _roadCollisionEventListener.OnEventHappened += HandleRoadCollision;
        }

        protected void OnDisable() {
            _roadCollisionEventListener.OnEventHappened -= HandleRoadCollision;
        }

        private void GenerateRoad() {
            _roadTransform = new List<Transform>();
            for (int i = 0; i < _initialRoadNumber + 1; i++) {
                var road = GetRoadFromStack();
                var position = new Vector3(0f, 0f, (i - 1) * _roadLength);
                //var road = Instantiate(_roadPrefab, position, Quaternion.identity);
                //_roadTransform.Add(road.transform);
                road.position = position;
                _roadTransform.Add(road);
            }
        }

        private GameObject CreateRoad() {
            var road = Instantiate(_roadPrefab, Vector3.zero, Quaternion.identity);
            road.SetActive(false);
            return road;
        }

        private void GeneratePool() {
            _stack = new Stack<Transform>();
            for (int i = 0; i < _initialStackRoadNumber; i++) {
                _stack.Push(CreateRoad().transform);
            }
        }

        private Transform GetRoadFromStack() {
            if (_stack.Count == 0) {
                _stack.Push(CreateRoad().transform);
            }

            var road = _stack.Pop();
            road.gameObject.SetActive(true);
            return road;
        }

        private void SetRoadToStack(Transform road) {
            road.gameObject.SetActive(false);
            _stack.Push(road);
        }

        private void HandleRoadCollision() {
            MoveRoadToStack();
            CreateLastPartOfRoad();
        }
        private void MoveRoadToStack() {
            var firstRoadPart = _roadTransform[0];
            _roadTransform.RemoveAt(0);
            //firstRoadPart.position = new Vector3(0f, 0f, _roadTransform[_roadTransform.Count - 1].position.z + _roadLength);
            //_roadTransform.Add(firstRoadPart)
            SetRoadToStack(firstRoadPart);
        }

        private void CreateLastPartOfRoad() {
            var road = GetRoadFromStack();
            road.position = new Vector3(0f, 0f, _roadTransform[_roadTransform.Count - 1].position.z + _roadLength);
            _roadTransform.Add(road);
        }
    }
}