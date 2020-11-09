using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameEditor {

    public class CarColorizer : EditorWindow {

        [MenuItem("Tools/Car colorizer")]
        public static void OpenWindow() {
            GetWindow<CarColorizer>("Car colorizer");
        }

        private Color _color = Color.white;

        private void OnGUI() {
            GUILayout.Label("Select color", EditorStyles.boldLabel);

            _color = EditorGUILayout.ColorField("Color",_color);

            if (GUILayout.Button("PressME")) {
                var gameObject = Selection.gameObjects;

                for (int i = 0; i < gameObject.Length; i++) {
                    //var meshRenderer=gameObject[i].GetComponent<MeshRenderer>();
                    if (gameObject[i].TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                        var material = new Material(meshRenderer.sharedMaterial);
                        material.color = _color;
                        meshRenderer.material=material;
                    }
                }
            }
        }
    }
}
