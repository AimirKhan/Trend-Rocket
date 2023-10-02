using App;
using UI.CapGame;
using UI.MainMenu;
using UI.SplashScreen;
using UnityEngine;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private MainGameManager mainGameManager;
        [SerializeField] private LoadingScreen loadingScreen;
        [Header("UI Elements")]
        [SerializeField] private MainMenuController mainMenuUi;
        [SerializeField] private CapGameController capGameUi;
        [SerializeField] private Game.CapGame capGame;
        
        private void OnEnable()
        {
            // Cap game Entry point
            loadingScreen.gameObject.SetActive(true);
            ShowMainMenu();
        }

        public void ShowMainMenu(bool menu = true)
        {
            loadingScreen.gameObject.SetActive(false);
            capGameUi.gameObject.SetActive(false);
            mainGameManager.gameObject.SetActive(false);
            capGame.gameObject.SetActive(false);
            mainMenuUi.gameObject.SetActive(menu);
        }
    
        public void ShowCapGame(bool cap = true)
        {
            mainMenuUi.gameObject.SetActive(false);
            capGameUi.gameObject.SetActive(cap);
            mainGameManager.gameObject.SetActive(cap);
            capGame.gameObject.SetActive(cap);
            loadingScreen.gameObject.SetActive(false);
        }
    }
}