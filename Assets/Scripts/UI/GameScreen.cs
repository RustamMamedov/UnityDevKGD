using UnityEngine;
using Events;

namespace UI {

    public class GameScreen : MonoBehaviour {

        private void OnDisable() {
            RenderManager.Instance.ReleaseTextures();
        }
    }
}
