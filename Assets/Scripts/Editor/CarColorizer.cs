using UnityEditor;
using UnityEngine;

namespace GameEditor {

    public class CarColorizer : EditorWindow {

        private Color _color;

        [MenuItem("Tools/CarColorizer")]
        private static void OpenWindow() {
            var window = GetWindow<CarColorizer>();
            window.titleContent = new GUIContent("CarColorizer");
            window.Show();
        }

        private void OnGUI() {
            GUILayout.Label("Select color", EditorStyles.boldLabel);

            _color = EditorGUILayout.ColorField("Color", _color);

            if (GUILayout.Button("Save")) {
                var gameObjects = Selection.gameObjects;
                foreach (GameObject gameObject in gameObjects) {
                    if (gameObject.TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                        var material = new Material(meshRenderer.sharedMaterial);
                        material.color = _color;
                        meshRenderer.material = material;
                    }

                }
            }
        }
    }
}
