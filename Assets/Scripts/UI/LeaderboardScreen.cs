using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    
    public class LeaderboardScreen : MonoBehaviour {

        // Fields.

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private GameObject _recordsList;

        [SerializeField]
        private GameObject _recordViewPrefab;


        // Life cycle.

        private void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnEnable() {
            var records = SaveManager.Instance.Records;
            int distinguishedIndex = SaveManager.Instance.NewRecordIndex;
            for (int i = 0; i < records.Length; ++i) {
                var record = records[i];
                var recordView = Instantiate(_recordViewPrefab, _recordsList.transform).GetComponent<RecordView>();
                recordView.SetData(i + 1, record.DateTime, record.Score, i == distinguishedIndex);
            }
        }

        private void OnDisable() {
            for (int i = 0; i < _recordsList.transform.childCount; ++i) {
                Destroy(_recordsList.transform.GetChild(i).gameObject);
            }
        }


        // Event handling.

        private void OnMenuButtonClick() {
            UIManager.Instance.LoadMenuScene();
        }


    }

}
