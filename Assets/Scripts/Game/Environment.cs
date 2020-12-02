using Events;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {
    
    public class Environment : MonoBehaviour {

        [SerializeField]
        private EventListener _roadCollisionEventListener;

        [SerializeField]
        [AssetsOnly]
        private GameObject _roadPrefab;

        [SerializeField]
        private int _initialRoadNumber = 10;

        [SerializeField]
        private int _initialStackRoadNumber = 5;

        [SerializeField]
        private int _roadLength = 12;

        private List<Transform> _roadTransforms;
        private Stack<Transform> _roadStack;

        private void Start() {
            GeneratePool();
            GenerateRoad();
        }

        private void OnEnable() {
            _roadCollisionEventListener.OnEventHappened += HandleRoadCollision;
        }

        private void OnDisable() {
            _roadCollisionEventListener.OnEventHappened -= HandleRoadCollision;
        }

        private void HandleRoadCollision() {
            MoveRoadToStack();
            CreateLastPartofRoad();
        }

        private void GeneratePool() {
            _roadStack = new Stack<Transform>();

            for (int i = 0; i < _initialStackRoadNumber + 1; i++) {
                _roadStack.Push(CreateRoad().transform);
            }
        }

        private void GenerateRoad() {
            _roadTransforms = new List<Transform>{};

            for (int i = 0; i < _initialRoadNumber + 1; i++) {
                var road = GetRoadFromStack();
                var position = new Vector3(0f, 0f, (i - 1) * _roadLength);

                road.position = position;
                _roadTransforms.Add(road);
            }
        }

        private void MoveRoadToStack() {
            var firstRoadPart = _roadTransforms[0];

            _roadTransforms.RemoveAt(0);
            SetRoadToStack(firstRoadPart);
        }

        private void CreateLastPartofRoad() {
            var road = GetRoadFromStack();

            road.position = new Vector3(0f, 0f, _roadTransforms[_roadTransforms.Count - 1].position.z + _roadLength);
            _roadTransforms.Add(road);
        }

        private GameObject CreateRoad(){
            var road = Instantiate(_roadPrefab, Vector3.zero, Quaternion.identity);
            road.SetActive(false);

            return road;
        }

        private Transform GetRoadFromStack() {
            if (_roadStack.Count == 0) {
                _roadStack.Push(CreateRoad().transform);
            }

            var road = _roadStack.Pop();
            road.gameObject.SetActive(true);
            return road;
        }

        private void SetRoadToStack(Transform road) {
            road.gameObject.SetActive(false);
            _roadStack.Push(road);
        }
    }
}
