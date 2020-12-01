﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class RenderManager : MonoBehaviour {

        public static RenderManager Instance;

        [SerializeField]
        private Camera _renderCamera;

        [SerializeField]
        private Transform _rootTransform;

        [SerializeField]
        private List<GameObject> _carDodgeViews = new List<GameObject>();

        private RenderTexture _texture;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public RenderTexture Render(GameObject prefab, Transform cameraTransform) {
            _renderCamera.transform.position = cameraTransform.position;
            var carInstance = Instantiate(prefab, _rootTransform);
            _texture = RenderTexture.GetTemporary(64, 64, 16);
            _texture.antiAliasing = 8;
            _texture.Create();
            _renderCamera.targetTexture = _texture;
            _renderCamera.Render();
            _renderCamera.targetTexture = null;
            Destroy(carInstance);
            return _texture;
        }
        public IEnumerator RenderCoroutine() {
            for (int i = 0; i < _carDodgeViews.Count; i++) {
                _carDodgeViews[i].GetComponent<CarDodgeView>().carImage.texture = Render(_carDodgeViews[i].GetComponent<CarDodgeView>().carSettings.renderCarPrefab, _carDodgeViews[i].GetComponent<CarDodgeView>().carSettings.renderCameraTransform);
                yield return null;
            }
        }
        public void ReleaseTextures() {
            RenderTexture.ReleaseTemporary(_texture);
        }
    }
}
