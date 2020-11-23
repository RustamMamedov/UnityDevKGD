using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Game;
using UnityEngine.UI;

namespace UI {
    public class GameScreen : MonoBehaviour {


        [SerializeField] private EventListener _collisionEventListener;

        [SerializeField]
        private List<CarDodgedView> _carDodgedViewList;

        [SerializeField]
        private List<Text> _carDodgedViewText;

        [SerializeField]
        private List<CarSettings> _carSettings;

        [SerializeField] private EventListener _carDodgedListerer;
        private void OnEnable() {
            StartCoroutine(CarRenderCoroutine());
            _collisionEventListener.OnEventHappened += ShowLeaderboardScreen;

            _carDodgedListerer.OnEventHappened += DodgeCounter;
        }

        private void ShowLeaderboardScreen() {
            for (var i = _carDodgedViewList.Count - 1; i > -1; i--) {
                _carSettings[i].differentCarCount = 0;
                _carDodgedViewText[i].text = "0";

            }
            UIManager.Instance.ShowLeaderboardScreen();
        }


        IEnumerator CarRenderCoroutine() {
            for (var i = _carDodgedViewList.Count - 1; i > -1; i--) {
                _carDodgedViewList[i].Init();
                yield return new WaitForEndOfFrame();
            }
        }

        public void DodgeCounter() {
            for (var i = 0; i < _carDodgedViewList.Count; i++) {
                _carDodgedViewText[i].text = _carSettings[i].differentCarCount.ToString();
            }
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTexture();
            _collisionEventListener.OnEventHappened -= ShowLeaderboardScreen;
            _carDodgedListerer.OnEventHappened -= DodgeCounter;
        }

    }
}