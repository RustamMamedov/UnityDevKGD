using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Game {

    public class Environment : MonoBehaviour {

        [SerializeField]
        private EventListener _roadCollisionEventListener;

        [SerializeField]
        private GameObject _roadPrefab;

        [SerializeField]
        private int _roadLength = 12;

        [SerializeField]
        private int _initialRoadNumber = 10;

        private List<Transform> _roadTransforms;

        private void Start() {
            GenerateRoad();
        }

        private void GenerateRoad() {
            _roadTransforms = new List<Transform>();
            for (int i = 0; i < _initialRoadNumber + 1; i++) {
                var position = new Vector3(0f, 0f, (i - 1) * _roadLength);
                var road = Instantiate(_roadPrefab, position, Quaternion.identity);
                _roadTransforms.Add(road.transform);
            }

            _roadPrefab.SetActive(false);
        }
    }

}
