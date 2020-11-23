using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Game;

namespace UI {

    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuB;

        [SerializeField]
        private GameObject _prefabResult;

        [SerializeField]
        private GameObject _GameObject;

        [SerializeField]
        private RecordView _recordView;

        [SerializeField]
        private ScriptableIntValue _numberRecord;

        private List<GameObject> _prefabs = new List<GameObject>();

        private void Awake() {
            _menuB.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnMenuButtonClick() {
            UIManager.Instance.LoadMenu();
        }

        private void OnEnable() {
            for (int i = 0; i < Save.SavedDatas.Count; i++) {
                _recordView.SetData(i + 1, Save.SavedDatas[i].date, Save.SavedDatas[i].score);
                if (i + 1 == _numberRecord.value) {
                    _recordView.isCurrentRecord = true;
                }
                else {
                    _recordView.isCurrentRecord = false;
                }
                GameObject Record = Instantiate(_prefabResult, _GameObject.transform);

                _prefabs.Add(Record);
            }
         }
        private void OnDisable() {
            for (int i = Save.SavedDatas.Count - 1; i > -1; i--) {
                Destroy(_prefabs[i]);
                _prefabs.RemoveAt(i);
            }
        }
    }
}