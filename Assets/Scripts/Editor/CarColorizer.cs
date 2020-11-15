using UnityEngine;
using UnityEditor;

namespace GameEditor {

    public class CarColorizer : EditorWindow {

        private Color _color = Color.white;
        
        [MenuItem("Tools/Car colorizer")]
        public static void OpenWindow() {
            GetWindow<CarColorizer>("Car colorizer");
        }

        private void OnGUI() {
            GUILayout.Label("Select color", EditorStyles.boldLabel);

            _color = EditorGUILayout.ColorField("Color", _color);

            if (GUILayout.Button("Press me")) {
                var gameObjects = Selection.gameObjects;
                for (int i = 0; i < gameObjects.Length; i++) {

                    var mechRenderer = gameObjects[i].GetComponent<MeshRenderer>();
                    if (gameObjects[i].TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                        //meshRenderer.sharedMaterial.color = _color; -- for every game object
                        //only for one
                        var material = new Material(meshRenderer.sharedMaterial);
                        material.color = _color;
                        meshRenderer.sharedMaterial = material;
                    }
                }
            }
        }

    }
}