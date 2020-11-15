using System;
using Game;
using UnityEngine;
using UnityEditor;

namespace GameEditor {
    
    [CustomEditor(typeof(EnemyCar))]
    public class EnemyCarHandle : Editor {

        private EnemyCar _enemyCar;
        
        private void OnEnable() {
            _enemyCar = (EnemyCar) target;
        }

        private void OnSceneGUI() {
            var enemyCar = (EnemyCar) target;
            if (Event.current.type == EventType.Repaint) {
                Handles.color = Color.red;

                var angle = 45;
                var rotation = Quaternion.Euler(0f, -angle / 2f, 0f);
                Handles.DrawSolidArc(_enemyCar.transform.position, Vector3.up, rotation* Vector3.forward, 45f, 5);
                
                var style = new GUIStyle();
                style.fontSize = 25;
                style.normal.textColor = Color.red;
                var text = $"{enemyCar.name}. Dodge score: {_enemyCar.CarSettings.dodgeScore}";
                Handles.Label(_enemyCar.transform.position + Vector3.up * 5f, text, style);
            }
        }
    }
}

