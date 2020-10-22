using Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    public class Environment : MonoBehaviour {
        [SerializeField]
        private EventListener _roadCollisionEventListener;

        [SerializeField]
        private GameObject _roadPrefab;

        [SerializeField]
        private int _initialRoadNumber = 10;

        [SerializeField]
        private int _roadLenght = 12;

        private List<Transform> _roadTransforms;
    }
}