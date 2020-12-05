using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;
using Game;

namespace Audio {
    public class Settings : MonoBehaviour {

        [SerializeField]
        Slider _sliderVolume;

        [SerializeField]
        private Button _dayButton;

        [SerializeField]
        private Button _nightButton;

        [SerializeField]
        private ScriptableIntValue _dayOrNight;

        private void Awake() {
            _dayButton.onClick.AddListener(OnDayButtonClick);
            _nightButton.onClick.AddListener(OnNightButtonClick);
        }

        private void OnDayButtonClick() {
            _dayOrNight.value = 1;
        }

        private void OnNightButtonClick() {
            _dayOrNight.value = 0;
        }
    }
}