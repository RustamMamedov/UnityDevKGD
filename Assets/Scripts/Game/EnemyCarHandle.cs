using UnityEngine;
using Game;
using UnityEditor;

namespace GameEditor { 
    [CustomEditor(typeof(EmenyCar))]
    public class EnemyCarHandle : Editor{

        private EmenyCar _emenyCar;
    
        private void OnEnable() {
            _emenyCar = (EmenyCar)target;
        }

        private void OnSceneGUI() {
            var enemyCar = (EmenyCar)target;

            if (Event.current.type == EventType.Repaint) {
                Handles.color = Color.red;

                var angle = 45f;
                var rotation = Quaternion.Euler(0f, -angle / 2, 0f);
                Handles.DrawSolidArc(_emenyCar.transform.position, Vector3.up, rotation*Vector3.forward, angle, 5f);


                var style = new GUIStyle();
                style.fontSize = 25;
                style.normal.textColor = Color.red;

                var text = $"{ _emenyCar.name }. Dodge score:{_emenyCar.CarSettings.dodgeScore}";
                Handles.Label(_emenyCar.transform.position + Vector3.up * 5f, text, style);
            }
        }
    }
}
