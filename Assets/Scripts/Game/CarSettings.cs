using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings", order = 0)]
    public class CarSettings : ScriptableObject {
        
        [Header("Score")]
        public int dodgeScore = 0;
        [Header("Speed")]
        public float maxSpeed;
        
        [Space]
        [Header("Acceleration")]
        public float acceleration;
    }
}
