using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UI;

namespace Game {
    [CreateAssetMenu(fileName = "CarsSettings", menuName = "CarsSettings")]
    public class CarsSettings : ScriptableObject {

        public bool isEnemyCar;
        [FoldoutGroup("Speed")]
        public float maxSpeed;
        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by accelaration every frame", InfoMessageType.Warning)]
        public float acceleration;

        [BoxGroup("Speed/Score")]
        [ValidateInput(nameof (ValidateDodgeScore))]
        public int dodgedScore;

        [Range(1f, 5f)]
        public float lenghLightCar;

        [ShowIfGroup(nameof(isEnemyCar))]
        [BoxGroup(nameof(isEnemyCar) + "/RenderSettings")]
        public GameObject renderCarPrefab;

        [BoxGroup(nameof(isEnemyCar) + "/RenderSettings/CameraRender")]
        public Vector3 positionCamera, rotationCamera;
        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }
    }
}
