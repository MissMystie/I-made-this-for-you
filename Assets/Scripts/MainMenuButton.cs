using Mystie.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mystie.UI
{
    public class MainMenuButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            GameManager.Instance.LoadMainMenu();
        }

        private void OnReset()
        {
            button = GetComponentInChildren<Button>();
        }
    }
}
