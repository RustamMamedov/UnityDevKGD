using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName="CarSettings", menuName="Game/CarSettings")]
    public class CarSettings : ScriptableObject {

        [BoxGroup("Score settings")]
        [ValidateInput(nameof(ValidateScore))]
        public int dodgeScore;

        [FoldoutGroup("Speed settings")]
        public float maxSpeed;

        [FoldoutGroup("Speed settings", false)]
        public float acceleration;


        private bool ValidateScore(int score) {
            return score >= 0;
        }


    }

}