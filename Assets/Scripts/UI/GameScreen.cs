﻿using UnityEngine;

namespace UI {

    public class GameScreen : MonoBehaviour {
        private void OnDisable() {
            RenderManager.Instanse.ReleaseTextures();
        }

    }
}