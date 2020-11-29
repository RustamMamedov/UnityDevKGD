using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour {
    public static VolumeSettings Instance;

    [SerializeField]
    private ScriptableFloatValue _volumeValue;

    private float _previousValue;

    public AudioSourcePlayer _mainTheme;

    [SerializeField]
    private Slider _musicSlider ;

    private void Start() {
        if (Instance != null) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
        _musicSlider.value = _mainTheme.audioSource.volume;
    }

    private void OnEnable() {
        _previousValue = _mainTheme.audioSource.volume;
    }

    private void Update() {
         _volumeValue.value = _musicSlider.value;
    }

    public void SetMainTheme(AudioClip audio) {
        _mainTheme.audioSource.clip = audio;
    }

    public void ResetVolume() {
        _volumeValue.value = _previousValue;
        _musicSlider.value = _volumeValue.value;
    }

    public void SaveMusicValue() {
        _volumeValue.value = _mainTheme.audioSource.volume;
        PlayerPrefs.SetFloat("MusicVolume", _volumeValue.value);
        Debug.Log(_volumeValue.value);   
    }
}
