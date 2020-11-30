using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Collections;
using System.Linq;
using UnityEngine;
using Events;
using Game;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private EventListener _gameSavedEventListener;

        [SerializeField]
        private GameObject _carDodgeViewPrefab;

        [SerializeField]
        private Transform _carDodgeViewParent;
        
        [SerializeField]
        [InfoBox("Cars settings for which it is required to display the dodge count.", InfoMessageType.Info)]
        [ValidateInput(nameof(CheckDuplicates), "Duplicate added.")]
        private List<CarSettings> _carSettings;

        private CarDodgeView[] _carDodgeViews;

        private bool CheckDuplicates() {
            return _carSettings.Distinct().Count() == _carSettings.Count;
        }

        private void Awake() {
            CarDodgeViewsCreate();
            _gameSavedEventListener.OnEventHappened += OnGameSaved;
        }

        private void OnEnable() {
            StartCoroutine(InitCarDodgeViews());
        }

        private void OnDisable() {
            for (int i = 0; i < _carDodgeViews.Length; i++) {
                RenderManager.Instance.ReleaseTextures();
            }
        }

        private void CarDodgeViewsCreate() {
            _carDodgeViews = new CarDodgeView[_carSettings.Count];
            for (int i = 0; i < _carDodgeViews.Length; i++) {

                var clone = Instantiate(_carDodgeViewPrefab, _carDodgeViewParent);
                var carDodgeView = clone.GetComponent<CarDodgeView>();
                carDodgeView.SetCarSettings(_carSettings[i]);

                _carDodgeViews[i] = carDodgeView;
            }
        }

        private IEnumerator InitCarDodgeViews() {
            for (int i = 0; i < _carSettings.Count; i++) {
                _carDodgeViews[i].Init();
                yield return new WaitForEndOfFrame();
            }
        }

        private void OnGameSaved() {
            UIManager.Instance.ShowLeaderboardsScreen();
        }
    }
}