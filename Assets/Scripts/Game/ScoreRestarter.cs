using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class ScoreRestarter : MonoBehaviour
{
    [SerializeField]
    private ScriptableIntValue _currentScore;
    [SerializeField]
    private CarSettings _suvDodgeScore;
    [SerializeField]
    private CarSettings _familyCarDodgeScore;
    [SerializeField]
    private CarSettings _truckDodgeScore;

    private void OnEnable() {
        _currentScore.value = 0;
        _suvDodgeScore.currentDodgeScore = 0;
        _familyCarDodgeScore.currentDodgeScore = 0;
        _truckDodgeScore.currentDodgeScore = 0;
    }
}
