using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Game{

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        public int dodgedScore;
        public float maxSpeed;
        public float acceleration;
        [CustomValueDrawer("MyCustomPositionLightCar")]
        public float lenghLightCar;

        private static float MyCustomPositionLightCar(float value) {
            return EditorGUILayout.Slider(value, 1f, 5f); 
        }
    }
}