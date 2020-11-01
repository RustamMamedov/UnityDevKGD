using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private ScriptableIntValue _score;

        private void Awake() {
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick() {
            _score.value = 0;
            UIManager.Instance.LoadGameplay();
        }
    }

}