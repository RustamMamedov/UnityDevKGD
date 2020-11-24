using Sirenix.OdinInspector;
using UnityEngine;
using Values;

namespace Game {

    [CreateAssetMenu(fileName="CarSettings", menuName="Game/CarSettings")]
    public class CarSettings : ScriptableObject {

        [BoxGroup("Scoring settings")]
        [ValidateInput(nameof(ValidateNonNegative))]
        public int dodgeScore;

        [BoxGroup("Scoring settings")]
        [ValidateInput(nameof(ValidateNonNegative))]
        public float dodgeDistance;

        [BoxGroup("Scoring settings")]
        public ScriptableIntValue dodgesCountValue;

        [FoldoutGroup("Speed settings")]
        public float maxSpeed;

        [FoldoutGroup("Speed settings", false)]
        public float acceleration;

        [BoxGroup("Icon rendering settings")]
        public GameObject renderableCarPrefab;

        [BoxGroup("Icon rendering settings")]
        [PropertyTooltip("The distance between rendered object and render camera.")]
        public float renderDistance;

        [BoxGroup("Other settings")]
        [PropertyRange(1f, 50f)]
        public float carLightDistance;

        private bool ValidateNonNegative(double value) {
            return value >= 0;
        }


    }

}