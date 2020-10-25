using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "CarsSettings", menuName = "CarsSettings")]
    public class CarsSettings : ScriptableObject {
        public int dodgedScore;
        public float maxSpeed;
        public float acceleration;
    }
}
