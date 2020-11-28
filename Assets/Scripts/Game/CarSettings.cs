using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        public bool isEnemy;

        [FoldoutGroup("Speed",false)]
        public int maxSpeed;
        [FoldoutGroup("Speed")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int acceleration;

        [ShowIf(nameof(isEnemy))]
        public string nameOfCar;

        [ShowIf(nameof(isEnemy))]
        public int currentDodgeScore;

        [ShowIf(nameof(isEnemy))]
        public int dodgeScore;

        [Range(1,5)]
        public int lightDistance;

        [ShowIf(nameof(isEnemy))]
        [FoldoutGroup("Rendering Parametres")]
        public Vector3 cameraPosition;

        [ShowIf(nameof(isEnemy))]
        [FoldoutGroup("Rendering Parametres")]
        public Quaternion cameraRotation;

        [ShowIf(nameof(isEnemy))]
        [FoldoutGroup("Rendering Parametres")]
        public GameObject renderCarPrefab;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }

    }
}