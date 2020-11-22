using System;
using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    public class CarDodgeView : MonoBehaviour {

        private int _carCounter = 0;

        [SerializeField] 
        private RawImage _carImage;

        [SerializeField] 
        private Text _carCounterText;

        [SerializeField] 
        private CarSettings _carSettings;

        [SerializeField] 
        private ScriptableStringValue _carDodgedName;

        [SerializeField] 
        private EventListener _carDodgedEventListener;

        private void OnEnable() {
            _carDodgedEventListener.OnEventHappened += OnCarDodgedBehaviour;
        }

        private void OnDisable() {
            _carDodgedEventListener.OnEventHappened -= OnCarDodgedBehaviour;
        }

        public void Init() {
            _carCounter = 0;
            _carCounterText.text = "0";
            _carImage.texture = RenderManager.Instance.Render(_carSettings.renderCarPrefab, 
                                 _carSettings.cameraPosition, _carSettings.cameraRotation);
        }

        private void OnCarDodgedBehaviour() {
            if (String.IsNullOrEmpty(_carDodgedName.value)) {
                return;
            }
            Debug.Log(_carDodgedName.value);

            if (_carSettings.name == _carDodgedName.value) {
                _carCounterText.text = $"{++_carCounter}";
                _carDodgedName.value = String.Empty;
            }
        }
    }
}