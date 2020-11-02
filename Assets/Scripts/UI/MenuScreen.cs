using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Events;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playButton;

        private void Awake() {

            _playButton.onClick.AddListener(OnPlayButtonClick);
        }

        public void OnPlayButtonClick() {
            UIManager.Instance.LoadGamePlay();
        }

    }
}
