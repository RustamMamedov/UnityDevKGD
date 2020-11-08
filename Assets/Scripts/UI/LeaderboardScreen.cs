using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UI { 

    public class LeaderboardScreen : MonoBehaviour {

        [SerializeField]
        private UIManager _uIManager;

        [SerializeField]
        private Button _menuButtom;

        [SerializeField]
        private ResoltView _resoltViewPrefab;

        [SerializeField]
        private GameObject _table;

        private void Awake() {
            _menuButtom.onClick.AddListener(OnMenuButtonClick);
        }

        private void OnEnable() {
            for (int i = 0; i < Save.SavedDatas.Count; i++) {
                var Resolt =Instantiate(_resoltViewPrefab,_table.gameObject.transform);
                Resolt.SetData(i+1, Save.SavedDatas[i].date, Save.SavedDatas[i].score);
            }
        }

        private void OnMenuButtonClick() {
            _uIManager.LoadMenu();
        }


    }
}