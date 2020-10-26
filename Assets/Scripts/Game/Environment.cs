using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {

    public class Environment : MonoBehaviour {

        [SerializeField]
        private EventListener _roadCollisionEventListener;

        [SerializeField]
        private GameObject _roadPrefab;

        [SerializeField]
        private int _initialRoadNumber = 10;

        [SerializeField]
        private int _roadLength = 12;

        private List<Transform> _roadTransforms;

        private void OnEnable() {
            _roadCollisionEventListener.OnEventHappened += HandleRoadCollsion;
        }

        private void OnDisable() {
            _roadCollisionEventListener.OnEventHappened -= HandleRoadCollsion;
        }

        private void Start() {
            GenerateRoad();
        }

        private void GenerateRoad() {
            _roadTransforms = new List<Transform>();

            for (int i = 0; i < _initialRoadNumber + 1; i++) {
                var positon = new Vector3(0f, 0f, (i - 1) * _roadLength);
                var road = Instantiate(_roadPrefab, positon, Quaternion.identity);             
                _roadTransforms.Add(road.transform);
            }
            _roadPrefab.SetActive(false);
        }

        private void HandleRoadCollsion() {
            MoveFirstRoadPart();
        }

        private void MoveFirstRoadPart() {
            var firstRoadPart = _roadTransforms[0];
            _roadTransforms.RemoveAt(0);
            firstRoadPart.position = new Vector3(0f, 0f, _roadTransforms[_roadTransforms.Count - 1].position.z + _roadLength);
            _roadTransforms.Add(firstRoadPart);
        }


    }
}
