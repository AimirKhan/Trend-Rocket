using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class PrivacyPolicy : MonoBehaviour
    {
        private MainMenuController mainMenuController;
        [SerializeField] private Button privacyPolicyStartButton;

        public bool IsPrivacyPolicyAccept { get; private set; }
        
        private void Awake()
        {
            mainMenuController = transform.parent.gameObject.GetComponent<MainMenuController>();
        }
        
        private void OnEnable()
        {
            privacyPolicyStartButton.onClick.AddListener(AcceptPrivacyPolicy);
        }

        private void AcceptPrivacyPolicy()
        {
            IsPrivacyPolicyAccept = true;
            mainMenuController.ChangeMenu(EMainMenuElements.MainMenu);
        }
        
        private void OnDisable()
        {
            privacyPolicyStartButton.onClick.RemoveListener(AcceptPrivacyPolicy);
        }
    }
}