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
                    var material = new Material(meshRenderer.material);
                    material.color = Color.red;
                    meshRenderer.material = material;
                }
            }
            if (GUILayout.Button("Red 2")) {
                if (enemyCar.TryGetComponent<MeshRenderer>(out var meshRenderer)) {
                    var material = new Material(meshRenderer.material);
                    material.color = new Color(0.5f, 0, 0);
                    meshRenderer.material = material;
                }
            }
            GUILayout.EndHorizontal();
        }


    }

}

