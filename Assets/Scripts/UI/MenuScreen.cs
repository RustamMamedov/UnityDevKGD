using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UI { 

    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private UIManager _uIManager;

        [SerializeField]
        private Button _playButtom;

        [SerializeField]
        private Button _optionsButtom;

        private void Awake() {
            _playButtom.onClick.AddListener(OnPlayButtonClick);
            _optionsButtom.onClick.AddListener(OnOptionsButtonClick);
        }

        private void OnPlayButtonClick() {
            _uIManager.LoadGameplay();
        }

        private void OnOptionsButtonClick() {
            _uIManager.ShowOptionScreen();
        }


    }
}