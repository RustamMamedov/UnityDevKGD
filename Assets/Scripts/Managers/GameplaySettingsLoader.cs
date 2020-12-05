using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class GameplaySettingsLoader : MonoBehaviour {
    [SerializeField]
    private GameObject _carLights;

    [SerializeField]
    private GameObject _sunLight;

    private void OnEnable() {
        if (SettingsScreen.GetInstance().getDayTime()) {
            _carLights.SetActive(true);
            _sunLight.SetActive(false);
            return;
        }
        _carLights.SetActive(false);
        _sunLight.SetActive(true);
    }

}
