using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;
using Game;
namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private EventListener _saveEventListener;

        [SerializeField]
        private EventListener _carDodgeEventListener;

        [SerializeField]
        private ScriptableStringValue _carDodgeName;

        [SerializeField]
        private List<CarSettings> _carSettings = new List<CarSettings>();

        [SerializeField]
        private List<CarDodgeView> _carDodgeViews = new List<CarDodgeView>();

        private void OnEnable() {
            _saveEventListener.OnEventHappened += ShowLeaderboardScreen;
            _carDodgeEventListener.OnEventHappened += DodgeCounter;
            StartCoroutine(InitCarDodgeViewCoroutine());
        }

        IEnumerator InitCarDodgeViewCoroutine() {
            for(int i = 0; i < _carDodgeViews.Count; i++) {
                _carDodgeViews[i].Init(_carSettings[i].renderCarPrefab, _carSettings[i].renderCameraPosition, _carSettings[i].renderCameraRotation);
                yield return new WaitForEndOfFrame();
            }
        }

        private void ShowLeaderboardScreen() {
            UIManager.Instance.ShowLeaderboardScreen();
        }

        private void DodgeCounter() {
            for(int i = 0; i < _carSettings.Count; i++) {
                if(_carSettings[i].name == _carDodgeName.value) {
                    _carDodgeViews[i].DodgeCounter();
                    break;
                }
            }
        }

        private void OnDisable() {
            _saveEventListener.OnEventHappened -= ShowLeaderboardScreen;
            _carDodgeEventListener.OnEventHappened -= DodgeCounter;
            RenderManager.Instance.ReleaseTextures();
        }
    }
}
