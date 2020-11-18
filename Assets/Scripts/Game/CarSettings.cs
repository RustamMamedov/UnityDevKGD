using Events;
using System;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "Car", menuName = "Car")]
    public class CarSettings : ScriptableObject {

        [Header("Score")]
        public int dodgeScore;
        [Header("Speed")]
        public float maxSpeed;
        [Space]
        public float acceleration;

    }
}