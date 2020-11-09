using Game;
using UnityEditor;
using UnityEngine;

namespace GameEditor {

    [CustomEditor(typeof(EnemyCar))]
    public class EnemyCarHandle : Editor {

        private EnemyCar _enemyCar;

        private void OnEnable() {
            _enemyCar = (EnemyCar) target;
        }

        private void OnSceneGUI() {
            if (Event.current.type == EventType.Repaint) {

                Handles.color = Color.red;
                var angle = 45f;
                var start = Quaternion.Euler(0, -angle / 2f, 0) * Vector3.forward;
                Handles.DrawWireArc(_enemyCar.transform.position, Vector3.up, start, angle, 5);

                var style = new GUIStyle();
                style.fontSize = 30;
                style.normal.textColor = Color.red;
                var text = $"{_enemyCar.name}. Dodge score: {_enemyCar.CarSettings.dodgeScore}";
                Handles.Label(_enemyCar.transform.position + 2 * Vector3.up, text, style);

            }
        }


    }

}

