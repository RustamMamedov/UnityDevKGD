using System;
using UnityEngine;
using UnityEngine.UI;
using UI;
using Game;
using Sirenix.OdinInspector;

namespace UI {
    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playButton;

        private void Awake() {

            _playButton.onClick.AddListener(OnPlayButtonClick);

        }

        public void OnPlayButtonClick() {

            UIManager.Instance.LoadGameplay();

        }
    }
}