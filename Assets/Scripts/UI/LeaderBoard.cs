using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class LeaderBoard : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private GameObject _recordViewPrefab;

        [SerializeField]
        private Transform _table;

        private void OnEnable() {

            for (int i = 0; i < Save.SavedDatas.Count; i++) {
                var recordLine = Instantiate(_recordViewPrefab, _table);
                recordLine.GetComponent<RecordView>().SetData(i + 1, Save.SavedDatas[i].date, Save.SavedDatas[i].score);
            
                if (i == Save.NewPlace) {
                    recordLine.GetComponent<RecordView>().FlowerImage.SetActive(true);
                }
            
            }
            
        }

        private void OnDisable() {
            
            for (int i = 0; i < Save.SavedDatas.Count; i++) { 
                Destroy(_table.GetChild(i).gameObject); 
            }
        
        }

        private void Awake() {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnMenuButtonClick() {
            UIManager.Instance.LoadMenu();
        }
    }
}