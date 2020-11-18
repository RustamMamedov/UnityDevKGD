using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {
    [CreateAssetMenu(fileName = "CarsSettings", menuName = "CarsSettings")]
    public class CarsSettings : ScriptableObject {

        [FoldoutGroup("Speed")]
        public float maxSpeed;
        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by accelaration every frame", InfoMessageType.Warning)]
        public float acceleration;

        [BoxGroup("Speed/Score")]
        [ValidateInput(nameof (ValidateDodgeScore))]
        public int dodgedScore;

        [Range(1f, 5f)]
        public float lenghLightCar;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}
