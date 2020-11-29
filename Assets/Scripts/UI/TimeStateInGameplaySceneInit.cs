using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class TimeStateInGameplaySceneInit : MonoBehaviour {

    [SerializeField]
    private ScriptableIntValue _timeState;

    [SerializeField]
    private Light _timeLightning;

    void Start() {
        switch (_timeState.value) {
            case 0:
                _timeLightning.intensity = 1;
                break;

            case 1:
                _timeLightning.intensity = 0;
                break;
        }
    }  
}
