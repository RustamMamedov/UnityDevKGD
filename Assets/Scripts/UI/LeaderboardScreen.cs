using System;
using UnityEngine;
using UnityEngine.UI;
using UI;
using Game;
using System.Collections.Generic;

public class LeaderboardScreen : MonoBehaviour
{
    [SerializeField]
    private Button _menuButton;

    [SerializeField]
    private GameObject _results;

    [SerializeField]
    private GameObject _resultView;

    private void Awake() {
        _menuButton.onClick.AddListener(OnMenuButtonClick);

    }

    private void OnEnable() {
        InitializeLeaderboard();
    }

    private void OnDisable() {
        DestroyLeaderboard();
    }

    public void OnMenuButtonClick() {
        UIManager.Instance.LoadMenu();
    }

    private void InitializeLeaderboard() {
        List<Save.SaveData> saves = Save.SavedDatas;
        for (int i = 0; i < saves.Count; i++) {
            GameObject newResultView = Instantiate(_resultView, _results.transform);

            if (saves[i].isHighlighted) {
                newResultView.transform.GetChild(0).gameObject.SetActive(true);
            }

            var recordView = newResultView.GetComponent<RecordView>();
            recordView.SetData(i+1, saves[i].date, saves[i].score);
        }
    }

    private void DestroyLeaderboard() {
        for (int i = _results.transform.childCount - 1; i >= 0; i--) {
            var recordView = _results.transform.GetChild(i);
            Destroy(recordView.gameObject);
        }
    }
}
