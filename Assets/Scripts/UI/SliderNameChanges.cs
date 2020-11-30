using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderNameChanges : MonoBehaviour
{

    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private string _nameValue1;

    [SerializeField]
    private float _value1;

    [SerializeField]
    private string _nameValue2;

    [SerializeField]
    private float _value2;

    [SerializeField]
    private Text _textLabel;

    private void OnEnable() {
        _slider.onValueChanged.AddListener(delegate { NameChanger(); });
        if (_slider.value == _value1)
            _textLabel.text = _nameValue1;
        if (_slider.value == _value2)
            _textLabel.text = _nameValue2;
    }


    public void NameChanger() {
        if (_slider.value == _value1)
            _textLabel.text = _nameValue1;
        if (_slider.value == _value2)
            _textLabel.text = _nameValue2;
    }

}
