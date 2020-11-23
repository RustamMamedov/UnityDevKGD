using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        private List<CarDodgedView> _listCarDodgedView;

        [SerializeField]
        private EventListeners _uppendCountCarDodgedView;

        private void OnEnable() {
            CarDodgedViewInit();
            SubscribleToEvent();
        }

        private void OnDisable() {
            UnSubscribleToEvent();
            RenderManager.Instance.ReleaseTexture();
        }

        private void CarDodgedViewInit() {
            StartCoroutine(CoroutineCarDodgedViewInit());
        }

        private void SubscribleToEvent() {
            _uppendCountCarDodgedView.OnEventHappened += SummCountDodgedViews;
        }

        private void UnSubscribleToEvent() {
            _uppendCountCarDodgedView.OnEventHappened -= SummCountDodgedViews;
        }

        private void SummCountDodgedViews() {
            for (int i = 0; i < _listCarDodgedView.Count; i++) {
                _listCarDodgedView[i].SummCountDodged();
            }
        }

        private IEnumerator CoroutineCarDodgedViewInit() {
            for (int i = 0; i < _listCarDodgedView.Count; i++) {
                _listCarDodgedView[i].OnInit();
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
