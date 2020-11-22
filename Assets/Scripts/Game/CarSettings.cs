using UnityEngine;
using Sirenix.OdinInspector;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        public bool isEnemyCar;

        [BoxGroup("Speed/Score")]
        [ValidateInput(nameof(VolidateDodgeScore))]
        [ShowIf(nameof(isEnemyCar))]
        public int dodgeScore;

        [BoxGroup("Speed", false)]
        [FoldoutGroup("Speed")]
        public float maxSpeed;

        [BoxGroup("Speed")]
        [Space]
        [InfoBox("Speed up")]
        public float acceleration;

        [Range(1f, 5f)]//Один из вариантов
        public float carLightLength;

        [BoxGroup("Render")]
        [ShowIf(nameof(isEnemyCar))]
        public GameObject renderCarPrefab;

        [BoxGroup("Render/Camera Settings")]
        [ShowIf(nameof(isEnemyCar))]
        public Vector3 cameraRenderPosition;

        [BoxGroup("Render/Camera Settings")]
        [ShowIf(nameof(isEnemyCar))]
        public Vector3 cameraRenderRotation;


        private bool VolidateDodgeScore() {
                return dodgeScore>0;
        }
        
    }
}