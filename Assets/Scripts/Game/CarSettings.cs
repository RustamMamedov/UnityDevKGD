using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "Car/CarSettings")]
    public class CarSettings : ScriptableObject {

        [BoxGroup("Score")]
        public int dodgeScore;
        
        [FoldoutGroup("Speed")]
        public float maxSpeed;
        [Space]
        [FoldoutGroup("Speed")]
        public float acceleration;
    }
}