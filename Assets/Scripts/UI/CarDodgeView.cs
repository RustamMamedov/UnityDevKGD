using UnityEngine;
using UnityEngine.UI;
using Game;
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
        private Text _counterText;

        private void OnEnable() {
            _update.OnEventHappened += UpdateBehaviour;
            _counter.value = 0;
            _counterText.text = "0";
        }

        private void OnDisable() {
            _update.OnEventHappened -= UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            _counterText.text = _counter.value.ToString();
        }

        public void Init() {
            _carImage.texture = RenderManager.Instance.Render(_carSettings);
        }
    }
}