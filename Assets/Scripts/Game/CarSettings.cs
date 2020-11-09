using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "NewCar", menuName ="Cars")]
    public class CarSettings : ScriptableObject {

        [Header("Score")]
        public int dodgeScore;
        [Header("Speed")]
        public float maxSpeed;
        public float acceleration;
    }

}
