using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName ="Car Settings",menuName = "Game/Car Settings")]
    public class CarSettings : ScriptableObject {

        public int dodgeScore;
        public float maxSpeed;
        public float acceleration;
    }
}

