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

            if (GUILayout.Button("PRESS ME")) {
                var gameObject = Selection.gameObjects;
                for (int i=0; i<gameObject.Length; i++) {
                    //if (gameObject[i].TryGetComponent<MeshRenderer>(out var meshRenderer)){
                        // meshRenderer.sharedMaterial.color = _color;
                        if (gameObject[i].TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                            var material = new Material(meshRenderer.sharedMaterial);
                            material.color = _color;
                            meshRenderer.sharedMaterial = material;
                        }

                    //}
                }
            }
        }
    }
}