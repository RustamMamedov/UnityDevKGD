using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        //[Header("Speed")]
        [FoldoutGroup("Speed", false)] //закрыт по умолчанию
        public float maxSpeed;
        //[Space]
        [FoldoutGroup("Speed")] //окошко, которое можно скрыть
        [InfoBox("Speed is beeng increased by acceleration every frame", InfoMessageType.Info)]//подсказка
        public float acceleration;

        [BoxGroup("Speed/Score")] //вложить список в список
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;
        [BoxGroup("Speed/Score")]
        public int dodgeScore2;

        [Range(1f,5f)]
        public float lightlenght;

        private bool ValidateDodgeScore(int score) {//защита от ошибки
            return score >= 0;
        }
 
    }
}