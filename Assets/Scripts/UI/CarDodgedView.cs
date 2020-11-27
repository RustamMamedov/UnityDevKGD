using UnityEngine;
using UnityEngine.UI;
using Game;
using Events;
using System;

namespace UI {

    public class CarDodgedView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private Text _countLabel;

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private EventListener _carDodgedEventListener;

        [SerializeField]
        private ScriptableStringValue _dodgedCarName;

        private int _countOfCars;

        private void OnEnable() {
            _carDodgedEventListener.OnEventHappened += OnCarDodged;
            _countOfCars = 0;
            _countLabel.text = _countOfCars.ToString();
        }

        private void OnDisable() {
            _carDodgedEventListener.OnEventHappened -= OnCarDodged;
        }

        private void OnCarDodged() {
            if (_dodgedCarName.name == _carSettings.name) {
                _countOfCars++;
                _countLabel.text = _countOfCars.ToString();
            }
        }

        public void Init(GameObject renderCarPrefs, Vector3 renderCameraPos) {
            _carImage.texture = RenderManager.Instance.Render(_carSettings.renderCarPrefab,_carSettings.renderCameraPosition);
        }
    }
}

