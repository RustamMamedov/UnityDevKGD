using UnityEditor;
using UnityEngine;
using Game;

namespace GameEditor {

    [CustomEditor(typeof(EmenyCar))]
    public class CarInspector : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            
            var enemyCar = (EmenyCar)target;

            GUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Red")) {
                if(enemyCar.TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                    meshRenderer.material.color = Color.red;
                }
            }
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Green")) {
                if (enemyCar.TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                    meshRenderer.material.color = Color.green;
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}