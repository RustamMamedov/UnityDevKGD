﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {
    public class Settings : MonoBehaviour {


        [SerializeField]
        private Button _lowButton;

        [SerializeField]
        private Button _hightButton;

        [SerializeField]
        private ScriptableIntValue _lowOrHight;

        [SerializeField]
        private Button _dayButton;

        [SerializeField]
        private Button _nightButton;

        [SerializeField]
        private ScriptableIntValue _dayOrNight;

        [SerializeField]
        private Button _okButton;

        [SerializeField]
        private Button _gancelButton;

        [SerializeField]
        private Slider _volume;

        private void Awake() {
            _lowButton.onClick.AddListener(OnLowButtonClick);
            _hightButton.onClick.AddListener(OnHightButtonClick);
            _dayButton.onClick.AddListener(OnDayButtonClick);
            _nightButton.onClick.AddListener(OnNightButtonClick);
           
            _okButton.onClick.AddListener(OnOkButtonClick);
            _gancelButton.onClick.AddListener(OnGancelButtonClick);
        }

        private void OnLowButtonClick() {
            _lowOrHight.value = 1;
        }

        private void OnHightButtonClick() {
            _lowOrHight.value = 0;
        }

        private void OnDayButtonClick() {
            _dayOrNight.value = 1;
        }

        private void OnNightButtonClick() {
            _dayOrNight.value = 0;
        }

        private void OnOkButtonClick() {
            SaveSettings.Instance.StartSaveProcess(_volume.value,_dayOrNight.value,_lowOrHight.value);
            UIManager.Instance.ShowMenuScreen();
        }

        private void OnGancelButtonClick() {
            SaveSettings.Instance.LoadSave();
            UIManager.Instance.ShowMenuScreen();
        }
    }
}