using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Game{

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        public bool isEnemyCar;

        [ShowIf(nameof(isEnemyCar))]
        public int dodgedScore;

        [FoldoutGroup("Speed",true)]
        public float maxSpeed;
        
        [FoldoutGroup("Speed")]
        public float acceleration;

        //[CustomValueDrawer(nameof(MyCustomPositionLightCar))]
        [BoxGroup("LenghLightCar")]
        public float lenghLightCar;

        [ShowIfGroup(nameof(isEnemyCar))]
        [BoxGroup(nameof(isEnemyCar)+ "/RenderSettings")]
        public GameObject renderCarPrefab;

        [BoxGroup(nameof(isEnemyCar) + "/RenderSettings/CameraRender")]
        public Vector3 positionCamera, rotationCamera;

        //private static float MyCustomPositionLightCar(float value) {
        //    return EditorGUILayout.Slider(value, 1f, 5f); 
        //}
    }
}