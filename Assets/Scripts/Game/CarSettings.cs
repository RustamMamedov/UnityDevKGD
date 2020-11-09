using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName="CarSettings", menuName="Game/CarSettings")]
    public class CarSettings : ScriptableObject {

        [Header("Score settings")]
        public int dodgeScore;

        [Header("Motion settings")]
        public float maxSpeed;
        public float acceleration;
        

    }

}