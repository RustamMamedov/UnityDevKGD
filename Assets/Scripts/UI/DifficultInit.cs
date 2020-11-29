using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class DifficultInit : MonoBehaviour
{
    [SerializeField]
    private ScriptableIntValue _difficultValue;

    private void Start() {
        _difficultValue.value = PlayerPrefs.GetInt("DifficultValue");
    }
}
