﻿using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "CarSettings", menuName = "Car/CarSettings")]
    public class CarSettings : ScriptableObject {

        [FoldoutGroup("Score")]
        [ValidateInput(nameof(ValidateDodgeScore))]
        public int dodgeScore;

        [FoldoutGroup("Speed")]
        public float maxSpeed;

        [FoldoutGroup("Speed")]
        [InfoBox("Speed is beeing increased by acceleration every frame", InfoMessageType.Warning)]
        public float acceleration;

        [FoldoutGroup("Car light")]
        [Range(0, 100)]
        public float lightRange;

        [FoldoutGroup("Car light")]
        [Range(0, 100)]
        public float lightAngle;

        [BoxGroup("Render settings")]
        public GameObject renderCarPrefab;

        [BoxGroup("Render settings")]
        public Quaternion cameraRotation;

        [BoxGroup("Render settings")]
        public Vector3 cameraPosition;

        [BoxGroup("Type of car")]
        public enum CarType {
            FamilyCar,
            SUV,
            Truck,
            SportCar,
        }

        [BoxGroup("Type of car")]
        public CarType carType;

        private bool ValidateDodgeScore(int score) {
            return score >= 0;
        }

    }
}
