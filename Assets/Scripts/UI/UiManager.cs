using App;
using UI.CapGame;
using UI.MainMenu;
using UI.SplashScreen;
using UnityEngine;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private OpenApp openApp;
    
        [Header("UI Elements")]
        [SerializeField] private MainMenuController mainMenuUi;
        [SerializeField] private CapGameController capGameUi;
        [SerializeField] private LoadingScreen loadingScreen;

        public MainMenuController MainMenuUi => mainMenuUi;
        public CapGameController CapGameUi => capGameUi;
        public LoadingScreen LoadingScreenUi => loadingScreen;

        private void OnEnable()
        {
            ShowLoadingScreen(); // On start app whatever cap or product
            openApp.HideNative += HideNative;
            openApp.OnOpenCap.AddListener(() => ShowMainMenu());
        }

        private void HideNative()
        {
            ShowMainMenu(false);
            ShowCapGame(false);
            ShowLoadingScreen(false);
        }

        public void ShowMainMenu(bool menu = true)
        {
            loadingScreen.gameObject.SetActive(false);
            capGameUi.gameObject.SetActive(false);
            mainMenuUi.gameObject.SetActive(menu);
        }
    
        public void ShowCapGame(bool cap = true)
        {
            mainMenuUi.gameObject.SetActive(false);
            capGameUi.gameObject.SetActive(cap);
            loadingScreen.gameObject.SetActive(false);
        }
    
        public void ShowLoadingScreen(bool splash = true)
        {
            loadingScreen.gameObject.SetActive(splash);
        }
    
        private void OnDisable()
        {
            openApp.HideNative -= HideNative;
            openApp.OnOpenCap.RemoveListener(() => ShowCapGame());
        }
    }
}