using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "Car setting", menuName = "Car setting")]
    public class CarSettings : ScriptableObject {
        
        public int dodgeScore;
        public int maxSpeed;
        public float acceleration;

    }
}