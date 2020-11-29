using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UnityEngine.UI;

public class LightningSettings : MonoBehaviour {

    public static LightningSettings Instance;

    [SerializeField]
    private Image _leftButtonImage;

    [SerializeField]
    private Image _rightButtonImage;

    [SerializeField]
    private ScriptableIntValue _timeState;

    private int _previousValue;

    private void Start() {
        if (Instance != null) {
            Destroy(this);
        }
        else {
            Instance = this;
        }     
    }

    private void OnEnable() {
        _previousValue = _timeState.value;
        switch (_timeState.value) {
            case 0:
                SetDay();
                break;

            case 1:
                SetNight();
                break;
        }
    }
    public void SetDay() {
        _timeState.value = 0;
        _leftButtonImage.color = new Color(_leftButtonImage.color.r, _leftButtonImage.color.g, _leftButtonImage.color.b, 1f);
        _rightButtonImage.color = new Color(_rightButtonImage.color.r, _rightButtonImage.color.g, _rightButtonImage.color.b, 0.3f);
    }
    public void SetNight() {
        _timeState.value = 1;
        _rightButtonImage.color = new Color(_rightButtonImage.color.r, _rightButtonImage.color.g, _rightButtonImage.color.b, 1f);
        _leftButtonImage.color = new Color(_leftButtonImage.color.r, _leftButtonImage.color.g, _leftButtonImage.color.b, 0.3f);
    }

    public void SaveTimeState() {
        PlayerPrefs.SetInt("TimeState", _timeState.value);
    }

    public void ResetTimeState() {
        _timeState.value = _previousValue;
    }

}
