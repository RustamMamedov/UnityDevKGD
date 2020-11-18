using Events;
using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "Car", menuName = "Car")]
    public class CarSettings : ScriptableObject {

        [BoxGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;

        [FoldoutGroup("Speed", false)]
        public float maxSpeed;
        [InfoBox("Speed is beeing increased by acceleration every frame", InfoMessageType.Warning)]
        [FoldoutGroup("Speed")]
        public float acceleration;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}