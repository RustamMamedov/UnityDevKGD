using System.Collections.Generic;
using UnityEngine;
using Events;
using Game;
using System.Collections;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private EventListener _carDodgeEventListener;

        [SerializeField]
        private ScriptableStringValue _carDodgeName;

        [SerializeField]
        private List<CarSettings> _carSettings = new List<CarSettings>();

        [SerializeField]
        private List<CarDodgeView> _carDodgeViews = new List<CarDodgeView>();

        private bool _canSetScore = true;

        private void OnEnable() {
            _carDodgeEventListener.OnEventHappened += DodgeCounter;
            StartCoroutine(InitCarDodgeView());
        }

        private void DodgeCounter() {
            if(_canSetScore == true) {
                for (int i = 0; i < _carSettings.Count; i++) {
                    if (_carSettings[i].name == _carDodgeName.value) {
                        _carDodgeViews[i].DodgeCounter();
                        StartCoroutine(CoolDown());
                        
                    }
                }
            }
        }

        private void OnDisable() {
            _carDodgeEventListener.OnEventHappened -= DodgeCounter;
            RenderManager.instance.ReleaseTextures();
        }

        IEnumerator InitCarDodgeView() {
            for (int i = 0; i < _carDodgeViews.Count; i++) {
                _carDodgeViews[i].Init(_carSettings[i].renderCarPrefab, _carSettings[i].cameraPos);
                yield return new WaitForEndOfFrame();
            }
        }

        IEnumerator CoolDown() {
            _canSetScore = false;
            yield return new WaitForSeconds(1f);
            _canSetScore = true;

        }
    }
}