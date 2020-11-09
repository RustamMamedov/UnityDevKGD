using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        [BoxGroup("Speed/Score")]
        [ValidateInput(nameof(VolidateDodgeScore))]
        public int dodgeScore;

        [BoxGroup("Speed", false)]
        [FoldoutGroup("Speed")]
        public float maxSpeed;

        [BoxGroup("Speed")]
        [Space]
        [InfoBox("Speed up")]
        public float acceleration;

        private bool VolidateDodgeScore() {
                return dodgeScore>0;
        }
        
    }
}