using UnityEditor;
using UnityEngine;
using Game;

namespace GameEditor {

    [CustomEditor(typeof(EnemyCar))]
    public class EnemyCarHandle : Editor {

        private EnemyCar _enemyCar;

        private void OnEnable() {

            _enemyCar = (EnemyCar)target;
        }

        private void OnSceneGUI() {
           /* if (Event.current.type == EventType.Repaint) {
                Handles.color = Color.red;

                var angle = 45f;
                var rotation = Quaternion.Euler(0, -angle / 2f, 0);
                Handles.DrawSolidArc(_enemyCar.transform.position, Vector3.up, rotation * Vector3.forward, 45, 5);


                var style = new GUIStyle();
                style.fontSize = 25;
                style.normal.textColor = Color.red;
                var text = $"{_enemyCar.name}. Dodge score: {_enemyCar.CarSettings.dodgeScore}";
                Handles.Label(_enemyCar.transform.position + Vector3.up * 5, text, style);

            }*/

        }

    }
}
