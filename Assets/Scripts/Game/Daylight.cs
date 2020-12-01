using UnityEngine;

namespace Game {

    public class Daylight : MonoBehaviour {

        [SerializeField]
        private ScriptableBoolValue _dayTimeGame;

        private void OnEnable() {
            if (!_dayTimeGame.value) {
                gameObject.SetActive(false);
            }
        }
    }
}