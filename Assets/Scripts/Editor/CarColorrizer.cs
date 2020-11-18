using UnityEditor;
using UnityEngine;

namespace GameEditor {

    public class CarColorrizer : EditorWindow {

        private Color _color= Color.white;

        [MenuItem("Tools/Car colorrizer")]
        public static void OpenWindow() {
            GetWindow<CarColorrizer>("Car colorrizer");
        }

        private void OnGUI() {
            GUILayout.Label("Select color", EditorStyles.boldLabel);

            _color =EditorGUILayout.ColorField("Color",_color);

            if (GUILayout.Button("Press me")) {
                var gameObject = Selection.gameObjects;
                for (int i = 0; i < gameObject.Length; i++) {
                    if (gameObject[i].TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                        var material = new Material(meshRenderer.sharedMaterial);
                        material.color = _color;
                        meshRenderer.material = material;
                    }
                }
            }
        }
    }
}