using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordView : MonoBehaviour
{
    [SerializeField]
    private GameObject _bestRideRecords;

    [SerializeField]
    private GameObject _recordView;



    public void SetData(int place,string date, string score) {
        var records = _bestRideRecords;
        records.transform.GetChild(0).GetComponent<Text>().text = place.ToString();
        records.transform.GetChild(1).GetComponent<Text>().text = date;
        records.transform.GetChild(2).GetComponent<Text>().text = score;
        
        Instantiate(records,_recordView.transform);
        
    }
}
