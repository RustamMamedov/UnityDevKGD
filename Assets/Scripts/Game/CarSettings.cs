using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    
    public class CarSettings : ScriptableObject {

        public int dodgeScore;
        public float maxSpeed;
        public float acceleration;
    }
}