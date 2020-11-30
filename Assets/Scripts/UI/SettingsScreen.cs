using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class SettingsScreen : MonoBehaviour {
        
        [SerializeField]
        private Button _okButton;


        private void Awake() {
            _okButton.onClick.AddListener(OnOkButtonClick);
        }

        private void OnOkButtonClick() {
            UIManager.Instance.ShowMenuScreen();
        }



    }
}