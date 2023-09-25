using System;
using UnityEngine;

namespace UI.MainMenu
{
    public enum EMainMenuElements
    {
        MainMenu,
        CharactersMenu,
        PrivacyPolicy
    }
    
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private UiManager uiManager;
        [SerializeField] private MainMenu mainMenu;
        [SerializeField] private CharactersMenu charactersMenu;
        [SerializeField] private PrivacyPolicy privacyPolicy;

        public UiManager UiManager => uiManager;
        
        private void OnEnable()
        {
            privacyPolicy.gameObject.SetActive(!privacyPolicy.IsPrivacyPolicyAccept);
        }

        public void ChangeMenu(EMainMenuElements elements)
        {
            privacyPolicy.gameObject.SetActive(true);
            switch (elements)
            {
                case EMainMenuElements.MainMenu:
                    mainMenu.gameObject.SetActive(true);
                    charactersMenu.gameObject.SetActive(false);
                    privacyPolicy.gameObject.SetActive(false);
                    break;
                case EMainMenuElements.CharactersMenu:
                    mainMenu.gameObject.SetActive(false);
                    charactersMenu.gameObject.SetActive(true);
                    privacyPolicy.gameObject.SetActive(false);
                    break;
                case EMainMenuElements.PrivacyPolicy:
                    mainMenu.gameObject.SetActive(false);
                    charactersMenu.gameObject.SetActive(false);
                    privacyPolicy.gameObject.SetActive(true);
                    break;
            }
        }
    }
}
