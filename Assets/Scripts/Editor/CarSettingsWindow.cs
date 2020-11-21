using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Game;


namespace GameEditor {

    public class CarSettingsWindow : OdinMenuEditorWindow {

        [MenuItem("Tools/Car Settings")]
        private static void OpenWindow() {
            GetWindow<CarSettingsWindow>().Show();
        }

        protected override OdinMenuTree BuildMenuTree() {

            var menuTree = new OdinMenuTree(supportsMultiSelect: false);
            menuTree.AddAllAssetsAtPath("Car Settings", "Assets/Resources/Cars Settings",typeof(CarSettings));
            return menuTree;
        }
    }
}

