using Game;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

namespace GameEditor {

    public class OdinWindow : OdinMenuEditorWindow {

        [MenuItem("My Game/My Window")]
        private static void OpenWindow() {
            GetWindow<OdinWindow>().Show();
        }

        protected override OdinMenuTree BuildMenuTree() {
            var tree = new OdinMenuTree();
            tree.Selection.SupportsMultiSelect = false;
            tree.AddAllAssetsAtPath("Car assets", "Assets/Resourses/Cars assets", typeof(CarSettings));
            return tree;
        }
    }
}