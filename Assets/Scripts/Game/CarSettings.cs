using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        [Header("Score")]
        public int dodgeScore;
        [Header("Speed")]
        public float maxSpeed;
        [Space]
        public float acceleration;
    }
}
