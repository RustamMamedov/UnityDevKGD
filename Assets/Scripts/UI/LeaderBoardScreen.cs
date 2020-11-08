using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {

    public class LeaderBoardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private GameObject _resultPrefab;

        [SerializeField]
        private GameObject _lighGroup;

        private List<GameObject> _listResult=new List<GameObject>();

        private void Awake() {
            _menuButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnEnable() {
            CreateAndSetDAtaToListResult();
        }

        private void OnDisable() {
            DeleteListResult();
        }

        private void CreateAndSetDAtaToListResult() {
            var length = Save.SavedDatas.Count;
            for (int i = 0; i < length; i++) {
                var result = Instantiate(_resultPrefab, _lighGroup.transform);
                _listResult.Add(result);
                result.GetComponent<RecordView>().SetData(i + 1, Save.SavedDatas[i].date, Save.SavedDatas[i].score);
                result.SetActive(true);
                if (Save.currentResult == Save.SavedDatas[i]) {
                    result.GetComponent<RecordView>().BG.enabled=true;
                }
            }
        }

        private void DeleteListResult() {
            var count = _listResult.Count;
            for (int i = count - 1; i >= 0; i--) {
                Destroy(_listResult[i]);
                _listResult.RemoveAt(i);
            }
        }

        private void OnPlayButtonClick() {
            UIManager.Instance.LoadMenu();
        }

    }
}
