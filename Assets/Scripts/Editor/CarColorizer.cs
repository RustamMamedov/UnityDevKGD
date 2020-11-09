using UnityEditor;
using UnityEngine;

namespace GameEditor {

    public class CarColorizer : EditorWindow {

        private Color _color = Color.white;

        [MenuItem("Tools/Car Colorizer")]
        public static void OpenWindow() {
            GetWindow<CarColorizer>("Car Colorizer");
        }

        private void OnGUI() {
            GUILayout.Label("Select color", EditorStyles.boldLabel);
            _color = EditorGUILayout.ColorField("Color of color", _color);
            if (GUILayout.Button("Button for button")) {
                var selected = Selection.gameObjects;
                foreach (var gameObject in selected) {
                    if (gameObject.TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                        var material = new Material(meshRenderer.material);
                        material.color = _color;
                        meshRenderer.material = material;
                    }
                }
            }
        }


    }

}

