using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "Car setting", menuName = "Car setting")]
    public class CarSettings : ScriptableObject {

        [Header("Score")]
        public int dodgeScore;

        [Header("Speed")]
        public int maxSpeed;
        [Space]
        public float acceleration;

    }
}