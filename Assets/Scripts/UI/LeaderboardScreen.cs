using UI;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardScreen : MonoBehaviour {
   
   [SerializeField]
   private Button _menuButton;

   private void Awake() {
      _menuButton.onClick.AddListener(OnPlayButtonClick);
   }

   private void OnPlayButtonClick() {
      UIManager.Instance.LoadMenu();
   }
}
