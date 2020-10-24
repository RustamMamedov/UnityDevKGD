using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Game {

    public class Enviroment : MonoBehaviour {

        [SerializeField]
        private EventListener _roadCollisionEventListener;

        [SerializeField]
        private GameObject _roadPrefab;

        [SerializeField]
        private int _initialRoadNumber = 10;

        [SerializeField]
        private int _roadLength = 12;

        private List<Transform> _roadTransforms;
    }
}
