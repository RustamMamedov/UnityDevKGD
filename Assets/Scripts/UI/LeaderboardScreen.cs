using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField] 
        private Button _menuButton;
        
        void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnDestroy() {
            _menuButton.onClick.RemoveListener(OnMenuButtonClick);
        }

        private void OnMenuButtonClick() {
            UIManager.Instance.LoadMenu();
        }
    }
}
