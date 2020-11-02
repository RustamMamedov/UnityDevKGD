using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Events;


namespace UI {
    public class LeaderBoradScreen : MonoBehaviour {



        [SerializeField]
        private Button _button;


        [SerializeField]
        private UIManager _uiManager;


     

        private void Awake() {
            _button.onClick.AddListener(OnPlayButtonClick);

        }



        private void OnPlayButtonClick() {
            _uiManager.LoadMenu();

        }



    }
}
