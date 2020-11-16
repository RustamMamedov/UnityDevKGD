using Events;
using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "Car", menuName = "Car")]
    public class CarSettings : ScriptableObject {

        [BoxGroup("Score")]
        public int dodgeScore;

        [FoldoutGroup("Score/Speed")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public float maxSpeed;
        [FoldoutGroup("Score/Speed", false)]
        [InfoBox("Speed is beeing increased by acceleartion every framy")]
        public float acceleration;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}
