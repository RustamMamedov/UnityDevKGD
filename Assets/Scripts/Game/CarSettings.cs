using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName ="Car Settings",menuName = "Game/Car Settings")]
    public class CarSettings : ScriptableObject {

        [Header("Score")]
        public int dodgeScore;
        [Header("Speed")]
        public float maxSpeed;
        [Space]
        public float acceleration;
    }
}

