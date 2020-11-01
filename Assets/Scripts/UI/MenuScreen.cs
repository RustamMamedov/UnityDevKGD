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
        private void Awake() {
            _playButtom.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick() {
            _uIManager.LoadGameplay();
        }


    }
}