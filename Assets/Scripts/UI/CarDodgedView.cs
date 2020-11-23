using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UI {

    public class CarDodgedView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField] 
        private EventListener _carDodgedEventListener;
        
        [SerializeField] 
        private Text _carsDodgedLabel;
       
        private int _carsDodgedCount;

        private void OnEnable() {
            SupscribeToEvents();
            SetToZeroLabelAndCount();
        }

        private void OnDisable() {
            UnsupscribeFromEvents();
            SetToZeroLabelAndCount();
        }

        private void SupscribeToEvents() {
            _carDodgedEventListener.OnEventHappened += OnCarDodged;
        }
        
        public void Init() {
            _carImage.texture = RenderManager.Instance.Render(_carSettings.renderCarPrefab, _carSettings.cameraPosition, _carSettings.cameraRotation);
        }

        private void SetToZeroLabelAndCount() {
            _carsDodgedCount = 0;
            _carsDodgedLabel.text = $"{_carsDodgedCount}";
        }
        
        private void UnsupscribeFromEvents() {
            _carDodgedEventListener.OnEventHappened -= OnCarDodged;
        }
        
        private void OnCarDodged() {
            StartCoroutine(IncrementDodgedCarsCoroutine());
        }

        private IEnumerator IncrementDodgedCarsCoroutine() {
                _carsDodgedCount++;
                _carsDodgedLabel.text = $"{_carsDodgedCount}";
                yield return null;
        } 
    }
}