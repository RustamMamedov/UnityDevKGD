﻿using System;
using UnityEngine;

namespace Game {

    [CreateAssetMenu(fileName = "ScriptableIntValue", menuName = "ScriptableIntValue")]
    public class ScriptableIntValue : ScriptableObject {
        public int value;
    }
}