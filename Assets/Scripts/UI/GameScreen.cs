using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

namespace UI {

    public class GameScreen : MonoBehaviour {

        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();
        }

    }

}
