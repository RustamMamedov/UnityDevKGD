using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class CarDodgeView : MonoBehaviour {


        [SerializeField]
        private RawImage _carImage;

        [SerializeField]
        private CarSettings _carSettings;

        [SerializeField]
        private Text _scoreLabel;

        private int _countDodgeCar=0;

        private void OnEnable() {
            Init();
        }

        public void Init() {
            _carImage.texture = RenderManager.Instance.Render(_carSettings.renderCarPrefab);
        }

        public void CheckDodgeId(ScriptableIntValue currentId) {
            if (_carSettings.id == currentId.value) {
                Debug.Log("CarDodgeType");
                _countDodgeCar++;
                _scoreLabel.text = $"{_countDodgeCar}";
            }
        }
    }
}