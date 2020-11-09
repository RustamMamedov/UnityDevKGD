using Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private GameObject _recordTable; 

        [SerializeField]
        private GameObject _resultViewPrefab;

        private List<GameObject> _bestResults;

        private void Awake() {
            _bestResults = new List<GameObject>();
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnEnable() {
            CreateTable();
        }

        private void OnDisable() {
            DeleteTable();
        }

        private void CreateTable() {
            var tableSize = Save.SavedDatas.Count;
            for (int i = 0; i < tableSize; i++) {
                var result = Instantiate(_resultViewPrefab, _recordTable.transform);
                result.GetComponent<RecordView>().SetData(i + 1, Save.SavedDatas[i].date, Save.SavedDatas[i].score);
                _bestResults.Add(result);
            }
        }

        private void DeleteTable() {
            for (int i = _bestResults.Count - 1; i > -1; i--) {
                Destroy(_bestResults[i]);
                _bestResults.RemoveAt(i);
            }
        }

        private void OnMenuButtonClick() {
            UIManager.Instance.LoadMenu();
        }
    }
}