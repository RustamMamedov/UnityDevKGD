using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "Car setting", menuName = "Car setting")]
    public class CarSettings : ScriptableObject {
        [SerializeField]
        public int _dodgeScore;
    }
}