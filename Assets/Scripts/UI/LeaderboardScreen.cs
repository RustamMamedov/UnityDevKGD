using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private RecordView recordView;



        private void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
            
            
        }

        private void OnEnable() {
            var savesDate = Save.SavedDatas;
            for (int i = 0; i < savesDate.Count; i++) {
                recordView.SetData(i,savesDate[i].date, savesDate[i].score);
            }
            
        }
        private void OnDisable() {
            int i = recordView.transform.GetChildCount();
            while (--i >= 0) {
                
                Destroy(recordView.transform.GetChild(i).gameObject);
                
            }
        }

        private void OnMenuButtonClick() {
            
            UIManager.Instance.LoadMenu();
            

        }
    }
}