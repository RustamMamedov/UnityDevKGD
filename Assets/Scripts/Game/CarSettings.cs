using Events;
using System;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "Car", menuName = "Car")]
    public class CarSettings : ScriptableObject {

        public int dodgeScore;
    }
}
