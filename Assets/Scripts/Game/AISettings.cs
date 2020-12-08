using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "AI Settings", menuName = "AISettings")]
    public class AISettings : ScriptableObject {

        [ValidateInput(nameof(CorrectnessDistance))]
        public float distance;

        private bool CorrectnessDistance() {
            return (distance > 0 && distance % 2 == 0);
        }
    }
}