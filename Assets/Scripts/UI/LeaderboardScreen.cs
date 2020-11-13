using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class LeaderboardScreen : MonoBehaviour {

    private List<GameObject> _recordViewOnScene = new List<GameObject>();

    [SerializeField]
    private GameObject _recordViewPrefab;

    [SerializeField]
    private GameObject _instantiatePosition;

    private void OnEnable() {
        int i = 1;
        foreach (var obj in Save.SaveDatas) {
            var record = Instantiate(_recordViewPrefab, _instantiatePosition.transform);
            _recordViewOnScene.Add(record);
            record.GetComponent<RecordView>().SetData(i, obj.date, obj.score);
            i++;
        }
    }

    private void OnDisable() {
        for (int i = 1; i <= _instantiatePosition.transform.childCount; i++) {
            Destroy(_instantiatePosition.transform.GetChild(i).gameObject);
        }
    }

}
