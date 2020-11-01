using UnityEngine;
using UnityEngine.UI;
using Events;
namespace UI {

    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuB;

        private void Awake() {
            _menuB.onClick.AddListener(OnMenuButtonClick);
        }
        private void OnMenuButtonClick() {
            UIManager.Instance.LoadMenu();
        }


    }

}