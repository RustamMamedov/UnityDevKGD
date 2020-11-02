using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "Car Assets", menuName = "Car/Settings")]
    public class CarSettings : ScriptableObject {

        [SerializeField]
        public int dodgeScore;
        public float maxSpeed;
        public float acceleration;
    }
}
