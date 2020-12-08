using Sirenix.OdinInspector;
using UnityEngine;


namespace Game {

        [CreateAssetMenu(fileName = "AISettings", menuName = "AISettings")]
        public class AISettings : ScriptableObject{
        [InfoBox("enter an even positive value", "ValidateDodgeScore", InfoMessageType = InfoMessageType.Error)]
        public int distance;

        private bool ValidateDodgeScore(int score) {
            return score < 0 || score % 2 == 1;
        }
    }
}
