using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UnityEngine.UI;

namespace UI {
    public class SettingsScreen : MonoBehaviour {
        [SerializeField]
        private ScriptableIntValue _dayOrNight;

        [SerializeField]
        private ScriptableIntValue _difficult;

        [SerializeField]
        private ScriptableFloatValue _volume;

        [SerializeField]
        private Button _btnOk;

        [SerializeField]
        private Button _btnCancel;

        [SerializeField]
        private Slider _sliderVolume;
        
        [SerializeField]
        private Slider _sliderDifficult;
        
        [SerializeField]
        private Slider _sliderDayOrNight;


        private void Awake() {
            

            
        }

        private void OnEnable() {
            _sliderDayOrNight.value = _dayOrNight.value;
            _sliderDifficult.value = _difficult.value;
            _sliderVolume.value = _volume.value;

            _btnOk.onClick.AddListener(OnOkButttonClick);
            _btnCancel.onClick.AddListener(OnCancelButttonClick);
            Debug.Log("Пипися");
        }


        private void OnOkButttonClick() {
            _dayOrNight.value = (int)_sliderDayOrNight.value;
            _difficult.value = (int)_sliderDifficult.value;
            _volume.value = _sliderVolume.value;
            UIManager.Instance.ShowMenuScreen();     
        }

        private void OnCancelButttonClick() {
            UIManager.Instance.ShowMenuScreen();
        
        }





    }

}
