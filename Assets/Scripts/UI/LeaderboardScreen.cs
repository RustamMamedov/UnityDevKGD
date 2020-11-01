using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UI { 

    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private UIManager _uIManager;

        [SerializeField]
        private Button _menuButtom;
        private void Awake() {
            _menuButtom.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnMenuButtonClick() {
            _uIManager.LoadGameplay();
        }


    }
}