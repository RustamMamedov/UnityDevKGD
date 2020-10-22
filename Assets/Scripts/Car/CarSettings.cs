using System;
using System.Collections.Generic;
using UnityEngine;
namespace Game{

    [CreateAssetMenu(fileName = "CarSettings", menuName = "CarSettings")]
    public class CarSettings : ScriptableObject {

        public int dodgedScore;
        public float maxSpeed;
        public float acceleration;
    }
}