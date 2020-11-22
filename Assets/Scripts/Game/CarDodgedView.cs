using UnityEngine;
using UnityEngine.UI;
using Game;
using Events;

namespace UI {

    public class CarDodgedView : MonoBehaviour {

        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private EventListener _update;

        [SerializeField]
        private Text _currentScore;

        [SerializeField]
        private ScriptableIntValue _score;

        private void OnEnable() {
            _update.OnEventHappened += UpdateBehaviour;
            _score.value = 0;
            _currentScore.text = "0";
        }

        private void OnDisable() {
            _update.OnEventHappened -= UpdateBehaviour;
        }

        private void UpdateBehaviour() {
            _currentScore.text = _score.value.ToString();
        }

        public void Init() {
            _carImage.texture = RenderManager.Instanse.Render(_carSettings);
         }
    }
}