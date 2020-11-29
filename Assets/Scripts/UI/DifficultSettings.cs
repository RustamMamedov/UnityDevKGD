using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UnityEngine.UI;

public class DifficultSettings : MonoBehaviour {

    public static DifficultSettings Instance;

    private int _previousValue;

    [SerializeField]
    private Image _leftButtonImage;

    [SerializeField]
    private Image _rightButtonImage;

    [SerializeField]
    public ScriptableIntValue _difficultValue;

    private void Start() {
        if (Instance != null) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    private void OnEnable() {
        _previousValue = _difficultValue.value;
        _difficultValue.value = PlayerPrefs.GetInt("DifficultValue");
        switch (_difficultValue.value) {
            case 0:
                SetEasy();
                break;

            case 1:
                SetHard();
                break;
        }
    }

    public void SetEasy() {
        _difficultValue.value = 0;
        _leftButtonImage.color = new Color(_leftButtonImage.color.r, _leftButtonImage.color.g, _leftButtonImage.color.b, 1f);
        _rightButtonImage.color = new Color(_rightButtonImage.color.r, _rightButtonImage.color.g, _rightButtonImage.color.b, 0.3f);
    }

    public void SetHard() {
        _difficultValue.value = 1;
        _rightButtonImage.color = new Color(_rightButtonImage.color.r, _rightButtonImage.color.g, _rightButtonImage.color.b, 1f);
        _leftButtonImage.color = new Color(_leftButtonImage.color.r, _leftButtonImage.color.g, _leftButtonImage.color.b, 0.3f);
    }

    public void SaveDifficultValue() {
        PlayerPrefs.SetInt("DifficultValue", _difficultValue.value);
    }

    public void ResetDifficultValue() {
        _difficultValue.value = _previousValue;
    }

}
