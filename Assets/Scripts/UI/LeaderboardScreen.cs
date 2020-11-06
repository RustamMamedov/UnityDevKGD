using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI
{
    public class LeaderboardScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private GameObject _recordPrefab;

        [SerializeField]
        private RecordView _recordView;

        [SerializeField]
        private GameObject _newRecordPrefab;

        [SerializeField]
        private RecordView _newRecordView;

        [SerializeField]
        private GameObject _records;

        [SerializeField]
        private ScriptableIntValue _indexOfNewRecord;

        private List<GameObject> _prefabs=new List<GameObject>();
        private void OnEnable()
        {
            if (_indexOfNewRecord.value < 10)
            {
                GameObject record;
                for (int i = 0; i < _indexOfNewRecord.value; i++)
                {
                    _recordView.SetData(i + 1, Save.SavedDatas[i].date, Save.SavedDatas[i].score);
                    record = Instantiate(_recordPrefab, _records.transform);
                    _prefabs.Add(record);
                }
                _newRecordView.SetData(_indexOfNewRecord.value + 1, Save.SavedDatas[_indexOfNewRecord.value].date, Save.SavedDatas[_indexOfNewRecord.value].score);
                record = Instantiate(_newRecordPrefab, _records.transform);
                _prefabs.Add(record);
                for (int i = _indexOfNewRecord.value+1; i < Save.SavedDatas.Count; i++)
                {
                    _recordView.SetData(i + 1, Save.SavedDatas[i].date, Save.SavedDatas[i].score);
                    record = Instantiate(_recordPrefab, _records.transform);
                    _prefabs.Add(record);
                }
            }
            else
            {
                for (int i = 0; i < Save.SavedDatas.Count; i++)
                {
                    _recordView.SetData(i + 1, Save.SavedDatas[i].date, Save.SavedDatas[i].score);
                    var record = Instantiate(_recordPrefab, _records.transform);
                    _prefabs.Add(record);
                }
            }
        }
        private void OnDisable()
        {
            for (int i = Save.SavedDatas.Count-1; i >-1 ; i--)
            {
                Destroy(_prefabs[i]);
                _prefabs.RemoveAt(i);
            }
        }
        private void Awake()
        {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }
        public void OnMenuButtonClick()
        {
            UIManager.Instance.LoadMenu();
        }
    }
}
