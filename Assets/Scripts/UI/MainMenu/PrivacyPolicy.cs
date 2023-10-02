using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class PrivacyPolicy : MonoBehaviour
    {
        private MainMenuController mainMenuController;
        [SerializeField] private Button privacyPolicyStartButton;
        [SerializeField] private bool isMainPrivacy;

        private string privacyKey = "privacy_accepted";

        public bool IsPrivacyPolicyAccepted { get; private set; }

        private void OnEnable() => privacyPolicyStartButton.onClick.AddListener(AcceptPrivacyPolicy);

        public void Init()
        {
            gameObject.SetActive(false);
            if (isMainPrivacy)
            {
                if (PlayerPrefs.GetInt(privacyKey, 0) == 0)
                    gameObject.SetActive(true);
                else
                    IsPrivacyPolicyAccepted = true;
            }
            else
            {
                gameObject.SetActive(true);
                mainMenuController = transform.parent.gameObject.GetComponent<MainMenuController>();
            }
        }

        private void AcceptPrivacyPolicy()
        {
            if (isMainPrivacy)
            {
                PlayerPrefs.SetInt("privacy_accepted", 1);
                IsPrivacyPolicyAccepted = true;
                gameObject.SetActive(false);
            }
            else
            {
                mainMenuController.ChangeMenu(EMainMenuElements.MainMenu);
            }
        }

        private void OnDisable() => privacyPolicyStartButton.onClick.RemoveListener(AcceptPrivacyPolicy);
    }
}