using Game;
using UnityEditor;
using UnityEngine;

namespace GameEditor {

    [CustomEditor(typeof(EnemyCar))]
    public class CarInspector : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            var enemyCar = (EnemyCar) target;

            GUILayout.BeginHorizontal();
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Red")) {
                if (enemyCar.TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                    var material = new Material(meshRenderer.sharedMaterial);
                    material.color = Color.red;
                    meshRenderer.material = material;
                }
            }

            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Green")) {
                if (enemyCar.TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                    var material = new Material(meshRenderer.sharedMaterial);
                    material.color = Color.green;
                    meshRenderer.material = material;
                }
            }
            GUILayout.EndHorizontal();
        }
    }

}
