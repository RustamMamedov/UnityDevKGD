using Game;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Events;

namespace UI {

    public class CarDodgeView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private float _dodgedNumberCountDelay;

        [SerializeField]
        private EventListener _updateEventListener;

        [SerializeField]
        private ScriptableIntValue _currentDodgedNumberValue;

        [SerializeField]
        private Text _dodgedNumberLabel;

        private int _currentDodgedNumber;
        private bool _isBusy;



        private void OnEnable() {
            _currentDodgedNumberValue.value = 0;
            _dodgedNumberLabel.text = "0";
            _currentDodgedNumber = 0;
            _updateEventListener.OnEventHappened += UpdateBehaviour;


        }

        private void OnDisable() {
            _updateEventListener.OnEventHappened -= UpdateBehaviour;
            _isBusy = false;
        }

        private void UpdateBehaviour() {
            if (_currentDodgedNumberValue.value > _currentDodgedNumber && !_isBusy) {
                StartCoroutine(SetScoreCoroutine(_currentDodgedNumberValue.value));
            }
        }

        private IEnumerator SetScoreCoroutine(int score) {
            _isBusy = true;
            while (_currentDodgedNumber < score) {
                _currentDodgedNumber++;
                _dodgedNumberLabel.text = $"{_currentDodgedNumber}";
                yield return new WaitForSeconds(_dodgedNumberCountDelay);
            }
            _isBusy = false;
        }


        public void Init() {
            var renderCarPrefab = _carSettings.renderCarPrefab;
            var renderCameraPosition = _carSettings.renderCameraPosition;
            var renderCameraRotation = _carSettings.renderCameraRotation;
            _carImage.texture = RenderManager.Instance.Render(renderCarPrefab, renderCameraPosition, renderCameraRotation);
        }
    }
}