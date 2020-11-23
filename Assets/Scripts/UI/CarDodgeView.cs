using Game;
using UnityEngine;
using UnityEngine.UI;
using Events;
using System.Collections.Generic;

namespace UI {

    public class CarDodgeView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private List<CarSettings> _carSettings;

        [SerializeField]
        private EventListener _carDodged;

        [SerializeField]
        private ScriptableIntValue _dodgedScore;

        private void OnEnable() {
            Init();
            SubscribeToEvents();
        }

        private void SubscribeToEvents() {
            _carDodged.OnEventHappened += CarDodged;
            
        }

        private void UnsubscribeToEvents() {
            _carDodged.OnEventHappened -= CarDodged;
           
        }

        private void CarDodged() {
            Debug.Log(_dodgedScore.value);
            if (_dodgedScore.value == 10) {
                _carImage.texture = RenderManager.Instance.Render(_carSettings[0].renderCarPrefab, _carSettings[0].renderCameraFOV);
            }
            else if (_dodgedScore.value == 20) {
                _carImage.texture = RenderManager.Instance.Render(_carSettings[1].renderCarPrefab, _carSettings[1].renderCameraFOV);
            }
            else {
                _carImage.texture = RenderManager.Instance.Render(_carSettings[2].renderCarPrefab, _carSettings[2].renderCameraFOV);
            }



        }

      

        public void Init() {
            _carImage.texture = RenderManager.Instance.Render(_carSettings[0].renderCarPrefab,_carSettings[0].renderCameraFOV);
        }

        
    }
}