using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "NewCar", menuName ="Cars")]
    public class CarSettings : ScriptableObject {

        public int dodgeScore;
        public float maxSpeed;
        public float acceleration;
    }

}
