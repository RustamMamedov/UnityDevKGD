using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "Car Assets", menuName = "Car/Settings")]
    public class CarSettings : ScriptableObject {

        [Header("Score")]
        public int dodgeScore;
        [Header("Speed")]
        public float maxSpeed;
        [Space]
        public float acceleration;
    }
}
}
