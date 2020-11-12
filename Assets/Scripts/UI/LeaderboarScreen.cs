using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;

namespace UI {
    public class LeaderboarScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private GameObject _resultViewPrefab;

        [SerializeField]
        private GameObject _list;
    
        private void OnEnable() {
            var curreentarrival = Save.IndexArrival;
            for (int i = 0; i < Save.SavedDatas.Count; i++) {
                var data = Save.SavedDatas[i];
                var createdThing = Instantiate(_resultViewPrefab, _list.transform);
                createdThing.GetComponent<RecordView>().SetData(i+1, data.date, data.score, i == curreentarrival);
            }
        }

        private void OnDisable() {
            for (int i = 0; i < _list.transform.childCount; i++) {
                Destroy(_list.transform.GetChild(i).gameObject);
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

