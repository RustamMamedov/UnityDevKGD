using Events;
using System;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "Car", menuName = "Car")]
    public class CarSettings : ScriptableObject {

        [Range(1, 5)]
        public int carLightDistance = 5;

        [BoxGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;

        [FoldoutGroup("Score/Speed")]
        public float maxSpeed;

        [FoldoutGroup("Score/Speed", false)]
        [InfoBox("Speed is beeing increased by acceleartion every framy")]
        public float acceleration;

        [BoxGroup("Render")]
        public GameObject renderCarPrefab;

        [BoxGroup("Render")]
        public Vector3 renderCameraPos;

        [BoxGroup("Render")]
        public Vector3 renderCameraRot;


        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}