using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UI.SettingsScreen;

namespace Game {
    public class LightChanger : MonoBehaviour {

        [SerializeField]
        private GameObject DayLight;

        [SerializeField]
        private List<GameObject> NightLight = new List<GameObject>();

        private const string RECORDS_KEY = "settings";


        [Serializable]
        private class SavedDataWrapper {
            public SaveData savedData;
        }
        private static SaveData _saveData;

        private void Awake() {

            LoadFromPlayerPrefs();

            if (_saveData.dayTime == 1) {
                DayMode();
            } else {
                NightMode();
            }
        }

        private void LoadFromPlayerPrefs() {
            var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
            _saveData = wrapper.savedData;

        }
        private void DayMode() {
            DayLight.SetActive(true);

            for (int i = 0; i < NightLight.Count; i++)
                NightLight[i].SetActive(false);
        }

        private void NightMode() {
            DayLight.SetActive(false);

            for (int i = 0; i < NightLight.Count; i++)
                NightLight[i].SetActive(true);
        }
    }
}



