using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        public enum CarType {
            Enemy,
            Player,
        }

        public CarType carType;

        [ValidateInput(nameof(ValidateDodgeScore))]
        [ShowIf("carType", CarType.Enemy)]
        public int dodgeScore;

        [FoldoutGroup("Speed", false)]
        public float maxSpeed;
        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by acceleration every frame", InfoMessageType.Info)]
        public float acceleration;

        [Range(1f, 5f)]
        [ShowIf("carType", CarType.Player)]
        public int headlightRange;

        #region Render

        [ShowIf("carType", CarType.Enemy)]
        [BoxGroup("Car Render")]
        public GameObject renderCarPrefab;

        [ShowIf("carType", CarType.Enemy)]
        [FoldoutGroup("Car Render/Camera Render", false)]
        public Vector3 position;
        
        [ShowIf("carType", CarType.Enemy)]
        [FoldoutGroup("Car Render/Camera Render", false)]
        public Quaternion rotation;

        #endregion Render

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}
