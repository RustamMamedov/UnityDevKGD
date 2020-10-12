using UnityEngine;
using System;

namespace Settings {
    [CreateAssetMenu(fileName = "Car", menuName = "Car")]
    public class CarSettings : ScriptableObject {

        public int dodgeScore;
    }
}