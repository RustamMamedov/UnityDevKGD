using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game;
using System;

namespace UI {

    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private Button _menuButton;

        [SerializeField]
        private GameObject _scorePrefab;

        [SerializeField]
        private VerticalLayoutGroup _verticalLayoutGroup;

        private Save _saveObject;
        private List<GameObject> _scores;

       


        private void Awake() {

            _menuButton.onClick.AddListener(OnMenuButtonClick);
            _scores = new List<GameObject>();
        }

        private void OnEnable() {
            _saveObject = GameObject.FindGameObjectWithTag("Save").GetComponent<Save>();
            var listForCreatingScoreTable = SortList(_saveObject);
            for (int i = 0; i < listForCreatingScoreTable.Count; i++) {

                var resultBlank = Instantiate(_scorePrefab);
                resultBlank.GetComponent<RecordsView>().SetData(i+1,listForCreatingScoreTable[i].data,listForCreatingScoreTable[i].score);
                resultBlank.transform.SetParent(_verticalLayoutGroup.transform);
                resultBlank.transform.localScale = new Vector3(1f,1f,1f);
                _scores.Add(resultBlank);
            }
            
        }

        private List<Save.SaveData>  SortList(Save save) {
            var sortedList = new List<Save.SaveData>();
            sortedList = save.SavedDatas;
            for (int i = 0; i < sortedList.Count-1; i++) {
                for (int j = i + 1; j < sortedList.Count; j++) {
                    if (Int32.Parse(sortedList[i].score) < Int32.Parse(sortedList[j].score)) {

                        var scoreCash = sortedList[i].score;
                        var dataCash = sortedList[i].data;
                        sortedList[i].score = sortedList[j].score;
                        sortedList[i].data = sortedList[j].data;
                        sortedList[j].score = scoreCash;
                        sortedList[j].data = dataCash;

                    }
                }
            }
            return sortedList;
        }

        private void OnDisable() {

            for (int i = _scores.Count - 1; i < 0; i--) {

                Destroy(_scores[i]);
                _scores.RemoveAt(i);
            }
            foreach (Transform child in _verticalLayoutGroup.transform) {
                Destroy(child.gameObject);
            }
        }

        private void OnMenuButtonClick() {

            UIManager.Instansce.LoadMenu();
        }
    }
}

