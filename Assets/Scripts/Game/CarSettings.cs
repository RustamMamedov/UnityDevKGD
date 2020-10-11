using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings", order = 0)]
    public class CarSettings : ScriptableObject {
        
        public int dodgeScore = 0;
    }
}
