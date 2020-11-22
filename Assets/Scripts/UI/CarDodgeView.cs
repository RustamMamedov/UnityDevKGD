using Game;
using UnityEngine;
using UnityEngine.UI;
using Events;

namespace UI {

    public class CarDodgeView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private EventListener _update;

        [SerializeField]
        private ScriptableIntValue _counter;

        [SerializeField]
        private Text _textCar;

        private void UpdateBehaviour() {
            _textCar.text = _counter.value.ToString();
        }

        private void OnEnable() {
            _update.OnEventHappened += UpdateBehaviour;
            _counter.value = 0;
            _textCar.text = "0";
        }

        private void OnDisable() {
            _update.OnEventHappened -= UpdateBehaviour;
        }

        public void Init() {
            _carImage.texture = RenderManager.Instance.Render(_carSettings);
        }
    }
}