using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI {
    public class LeaderboardScreen : MonoBehaviour{
        [SerializeField]
        private Button _menu;

        [SerializeField]
        private UIManager _uiManager;

        private void Awake() {
            _menu.onClick.AddListener(OnPlayButtonClick);
        
        }

        private void OnPlayButtonClick() {
            _uiManager.LoadMenu();

        }
    }
}

