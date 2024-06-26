using System;
using Events;
using Services;
using UI.FinalScreen;
using UI.MainGame;
using UniRx;
using UnityEngine;

namespace UI.CapGame
{
    public enum EGamePanels
    {
        GameScreen,
        PauseScreen,
        FinalScreen
    }
    public class CapGameController : MonoBehaviour
    {
        [SerializeField] private UiManager uiManager;
        [SerializeField] private FinalScreenManager finalScreen;
        [SerializeField] private PauseMenu pauseMenu;

        private void Awake()
        {
            GlobalVariables.Instance.GameState
                .Where(ctx => ctx == EGameState.Finished)
                .Subscribe(ctx => ChangeGameScreen(EGamePanels.FinalScreen))
                .AddTo(this);
        }

        public void ChangeGameScreen(EGamePanels gamePanel = EGamePanels.GameScreen)
        {
            switch (gamePanel)
            {
                case EGamePanels.GameScreen:
                    finalScreen.gameObject.SetActive(false);
                    pauseMenu.gameObject.SetActive(false);
                    break;
                case EGamePanels.FinalScreen:
                    finalScreen.gameObject.SetActive(true);
                    pauseMenu.gameObject.SetActive(false);
                    break;
                case EGamePanels.PauseScreen:
                    finalScreen.gameObject.SetActive(false);
                    pauseMenu.gameObject.SetActive(true);
                    break;
            }
        }

        public void GoToMainMenu()
        {
            ChangeGameScreen();
            uiManager.ShowMainMenu();
        }
    }
}
