using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "CarsSettings", menuName = "CarsSettings")]
    public class CarsSettings : ScriptableObject {
        [Header("Score")]
        public int dodgedScore;
        [Header("Speed")]
        public float maxSpeed;
        [Space]
        public float acceleration;
        [Range(1f,5f)]
        public float lenghLightCar;
    }
}
