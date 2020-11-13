using Events;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    public class Enviroment : MonoBehaviour {

        [SerializeField] 
        private EventListener _roadCollisionEventListener;
        
        [SerializeField] 
        [AssetsOnly]
        private GameObject _roadPrefab;

        [SerializeField] 
        private int _initialRoadNumber = 10;

        [SerializeField] 
        private int _roadLenght = 12;

        private List<Transform> _roadTransform;

        private void Start() {
            GenerateRoad();
        }

        private void OnEnable() {
            _roadCollisionEventListener.OnEventHappened += HandleRoadCollision;
        }
        private void OnDisable() {
            _roadCollisionEventListener.OnEventHappened -= HandleRoadCollision;
        }
        
        
        private void GenerateRoad() {
            _roadTransform = new List<Transform>();
            for (int i = 0; i < _initialRoadNumber + 1; i++) {
                var position = new Vector3(0f,0f,(i - 1) * _roadLenght);
                
                var road = Instantiate(_roadPrefab, position, Quaternion.identity);
                _roadTransform.Add(road.transform);
            }
        }

        private void HandleRoadCollision() {
            MoveFirstRoadPart();
        }

        private void MoveFirstRoadPart() {
            var firstRoadPart = _roadTransform[0];
            _roadTransform.RemoveAt(0);
            firstRoadPart.position = new Vector3(0f,0f,_roadTransform[_roadTransform.Count - 1].position.z + _roadLenght);
            _roadTransform.Add(firstRoadPart);
        }
    }
    
}