using Game;
using Events;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private EventListener _resultsSavedEventListener;

        [SerializeField] 
        private GameObject _carDodgeViewPrefab; 

        [SerializeField]
        private GameObject _carDodgeViewWrapper;

        [SerializeField] 
        private List<GameObject> _carDodgeViews = new List<GameObject>{}; 

        [SerializeField] 
        private List<CarSettings> _carSettingsList = new List<CarSettings>{}; 

        private void OnEnable() {
            _resultsSavedEventListener.OnEventHappened += OnResultsSaved;
            StartCoroutine(CarDodgeViewCoroutine());
        }

        private void OnResultsSaved() {
            UIManager.Instance.ShowLeaderboardScreen();
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();
            for (int i = _carDodgeViews.Count - 1; i >= 0; i--) {
                Destroy(_carDodgeViews[i]);
                _carDodgeViews.RemoveAt(i);
            }
        }

        private IEnumerator CarDodgeViewCoroutine() {
            for (int i = 0; i < _carSettingsList.Count; i++) {  
                var carDodge = Instantiate(_carDodgeViewPrefab, _carDodgeViewWrapper.transform);
                carDodge.GetComponent<CarDodgeView>().Init(_carSettingsList[i].renderCarPrefab, _carSettingsList[i].cameraPosition, _carSettingsList[i].cameraRotation); 
                _carDodgeViews.Add(carDodge);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}