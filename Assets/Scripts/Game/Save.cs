using System.Collections.Generic;
using UnityEngine;
using Events;
using System;

namespace Game {


    public class Save : MonoBehaviour {

	[Serializable]
	public class SaveData {

	    public string date;
	    public string score;
	}

	[Serializable]
	private class SavedDataWrapper {

	    public List<SaveData> saveDatas;
	}

	[SerializeField]
	private EventListener _carCollisionEnetListener;

	[SerializeField]
	private ScriptableIntValue _currentScore;

	private List<SaveData> _saveDatas;
	public List<SaveData> SavedDatas => _saveDatas; 
 
	private const string RECORDS_KEY = "records";

	private void Awake() {
	    _saveDatas = new List<SaveData>();
	    LoadFromPlayerPrefs();
	}

	private void OnEnable() {
	    _carCollisionEnetListener.OnEventHappend += onCarCollision;    
	}

	private void OnDisable() {
	    _carCollisionEnetListener.OnEventHappend -= onCarCollision;
	}

	private void onCarCollision() {

	    var newRecord = new SaveData {
		date = DateTime.Now.ToString("MM/dd/yyyy HH:mm"),
		score = _currentScore.value.ToString()
	    };
	    Debug.Log($"new record:{newRecord.date} {newRecord.score}");
	    _saveDatas.Add(newRecord);

	    SaveDataToPlayerPrefs();
	}

	private void LoadFromPlayerPrefs() {
	    if (!PlayerPrefs.HasKey(RECORDS_KEY)) {
		return;
	    }

	    var wrapper = JsonUtility.FromJson<SavedDataWrapper>(PlayerPrefs.GetString(RECORDS_KEY));
	    _saveDatas = wrapper.saveDatas;
	}

	private void SaveDataToPlayerPrefs() {
	    var wrapper = new SavedDataWrapper {
		saveDatas = _saveDatas
	    };
	    var json = JsonUtility.ToJson(wrapper);
	    PlayerPrefs.SetString(RECORDS_KEY, json);

	}
    }
}
