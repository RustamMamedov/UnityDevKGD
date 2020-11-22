using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Game;

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
            tree.AddAllAssetsAtPath("Score", "Assets/Resourses/ScriptableValues", typeof(ScriptableIntValue));    
            return tree;
        }
    }
}