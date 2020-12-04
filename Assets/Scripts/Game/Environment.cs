using System;
using System.Collections.Generic;
using Events;
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
        private GameObject _directionalLight;

        [SerializeField]
        private int _initialRoadNumber = 10;
        
        [SerializeField]
        private int _initialStackRoadNumber = 5;

        [SerializeField]
        private int _roadLength = 12;

        private List<Transform> _roadTransforms;
        private Stack<Transform> _stack;

        private void Awake() {
            if (PlayerPrefs.GetInt("SavedLight")==0) {
                _directionalLight.SetActive(true);
            }
            else {
                _directionalLight.SetActive(false);
            }
        }

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
        private void GenerateRoad() {
            _roadTransforms = new List<Transform>();

            for (int i = 0; i < _initialRoadNumber + 1; i++) {
                var road = GetRoadFromStack();
                var position = new Vector3(0f, 0f, (i - 1) * _roadLength);
                road.transform.position = position;
                _roadTransforms.Add(road);
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
            CreateRoadLastPart();
           // MoveFirstRoadPart();
        }

        private void MoveRoadToStack() {
            var firstRoadPart = _roadTransforms[0];
            _roadTransforms.RemoveAt(0);
            SetRoadToStack(firstRoadPart);
        }

        private void CreateRoadLastPart() {
            var road = GetRoadFromStack();
            road.position = new Vector3(0f, 0f, _roadTransforms[_roadTransforms.Count - 1].position.z + _roadLength);
            _roadTransforms.Add(road);
        }

    //    private void MoveFirstRoadPart() {

    //        var firstRoadPart = _roadTransforms[0];
    //        _roadTransforms.RemoveAt(0);
    //        firstRoadPart.position = new Vector3(0f, 0f, _roadTransforms[_roadTransforms.Count - 1].position.z + _roadLength);
    //        _roadTransforms.Add(firstRoadPart);
    //    }
    }

}
