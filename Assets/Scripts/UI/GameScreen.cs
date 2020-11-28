using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace UI {

    public class GameScreen : MonoBehaviour {

        [SerializeField]
        List<CarDodgeView> _carDodgeViews = new List<CarDodgeView>();

        private void OnEnable() {
             StartCoroutine(RenderTextureInit());
        }

        IEnumerator RenderTextureInit() {
            foreach (var obj in _carDodgeViews) {
                obj.Init();
                yield return null;
            }
        }

        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();
        }
    }
}
