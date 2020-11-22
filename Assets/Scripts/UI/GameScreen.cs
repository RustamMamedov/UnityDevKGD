using System.Collections;
using System.Collections.Generic;
using Events;
using Game;
using UnityEngine;

namespace UI {
    
    public class GameScreen : MonoBehaviour {

        [SerializeField] 
        private List<CarDodgeView> _carDodgeViews = new List<CarDodgeView>();

        private void OnEnable() {
            StartCoroutine(InitCarDodgeViewCoroutine());
        }

        private IEnumerator InitCarDodgeViewCoroutine() {
            foreach(var x in _carDodgeViews) {
                x.Init();
               yield return new WaitForEndOfFrame();
            }
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();
        }
    }
}


