using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "New Car", menuName = "Car")]
    public class CarSettings : ScriptableObject {
        
        public int dodgeScore;
        public float maxSpeed;
        public float acceleration;

    }
}

