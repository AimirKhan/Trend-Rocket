using System;
using UI.FinalScreen;
using UI.MainGame;
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
        [SerializeField] private MainGameManager mainGame;
        [SerializeField] private FinalScreenManager finalScreen;
        [SerializeField] private PauseMenu pauseMenu;

        public void ChangeGameScreen(EGamePanels gamePanel = EGamePanels.GameScreen)
        {
            switch (gamePanel)
            {
                case EGamePanels.GameScreen:
                    mainGame.gameObject.SetActive(true);
                    finalScreen.gameObject.SetActive(false);
                    pauseMenu.gameObject.SetActive(false);
                    break;
                case EGamePanels.FinalScreen:
                    mainGame.gameObject.SetActive(true);
                    finalScreen.gameObject.SetActive(true);
                    pauseMenu.gameObject.SetActive(false);
                    break;
                case EGamePanels.PauseScreen:
                    mainGame.gameObject.SetActive(true);
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
