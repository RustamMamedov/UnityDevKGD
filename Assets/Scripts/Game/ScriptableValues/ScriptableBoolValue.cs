using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "ScriptableBoolValue", menuName = "ScriptableBoolValue")]
    public class ScriptableBoolValue : ScriptableObject {
        public bool value;
    }
}