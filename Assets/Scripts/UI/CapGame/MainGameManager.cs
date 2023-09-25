using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.CapGame
{
    public class MainGameManager : MonoBehaviour
    {
        [SerializeField] private CapGameController capGameController;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button downButton;
        [SerializeField] private Button upButton;

        private void OnEnable()
        {
            pauseButton.onClick.AddListener(ShowPauseMenu);
        }

        private void ShowPauseMenu()
        {
            capGameController.ChangeGameScreen(EGamePanels.PauseScreen);
        }
        
        private void OnDisable()
        {
            pauseButton.onClick.AddListener(ShowPauseMenu);
        }
    }
}