using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "AI Settings",menuName = "new AI Settings")]
    public class AIsettings : ScriptableObject {

        [ValidateInput(nameof(ValidateDistace))]
        public float distance;

        private bool ValidateDistace() {
            return (distance > 0 && distance % 2 == 0) ? true : false;
        }
    }
}