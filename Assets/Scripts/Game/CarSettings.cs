﻿using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {
        [BoxGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;
        [FoldoutGroup("Speed")]
        public float maxSpeed;
        [FoldoutGroup("Speed")]
        public float acceleration;

        private bool ValidateDodgeScore(int score)
        {
            return score >= 0;
        }
    }
}