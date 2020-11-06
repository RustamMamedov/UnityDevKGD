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
        private GameObject _records;
        private List<GameObject> _prefabs=new List<GameObject>();
        private void OnEnable()
        {
            for(int i = 0; i < Save.SavedDatas.Count; i++)
            {

                _recordView.SetData(i + 1, Save.SavedDatas[i].date, Save.SavedDatas[i].score);
                var record = Instantiate(_recordPrefab, _records.transform);
                _prefabs.Add(record);
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
