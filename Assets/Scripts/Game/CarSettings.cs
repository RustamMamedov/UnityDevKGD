using Events;
using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "Car", menuName = "Car")]
    public class CarSettings : ScriptableObject {

        [Range(0, 5)]
        public int carLightDistance = 5;

        [BoxGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;

        [FoldoutGroup("Score/Speed")]
        public float maxSpeed;

        [FoldoutGroup("Score/Speed", false)]
        [InfoBox("Speed is beeing increased by acceleartion every framy")]
        public float acceleration;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}
