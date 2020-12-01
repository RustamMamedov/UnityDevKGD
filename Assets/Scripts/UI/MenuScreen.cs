using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class MenuScreen : MonoBehaviour {

        [SerializeField]
        private Button _playClickButton;

        [SerializeField]
        private Button _sattingsButton;

        private void Awake() {
            _playClickButton.onClick.AddListener(OnPlayButtonClick);
            _sattingsButton.onClick.AddListener(OnSattingsButtonClick);
        }

        private void OnPlayButtonClick() {
            UIManager.Instance.LoadGamePlay();
        }

        private void OnSattingsButtonClick() {
            StartCoroutine(OnSattingsButtonClickCoroutine());
        }

        private IEnumerator OnSattingsButtonClickCoroutine() {
            yield return new WaitForSeconds(0.2f);
            UIManager.Instance.ShowSattingsScreen();
        }
    }
}
