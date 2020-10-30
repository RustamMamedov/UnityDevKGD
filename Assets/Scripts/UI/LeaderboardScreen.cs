using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LeaderboardScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _menuButton;

        private void Awake()
        {
            _menuButton.onClick.AddListener(OnMenuButtonClick);
        }
        public void OnMenuButtonClick()
        {
            UIManager.Instance.LoadMenu();
        }
    }
}
