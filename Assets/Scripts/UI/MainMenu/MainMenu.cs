using System.Collections.Generic;
using Events;
using Game;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        private MainMenuController mainMenuController;
        [SerializeField] private CharactersMenu charactersMenu;
        [SerializeField] private Image heroImage;
        [Header("Buttons")]
        [SerializeField] private Button startButton;
        [SerializeField] private Button charactersButton;
        [SerializeField] private Button privacyPolicyButton;

        private void Awake()
        {
            mainMenuController = transform.parent.gameObject.GetComponent<MainMenuController>();
        }

        private void OnEnable()
        {
            startButton.onClick.AddListener(StartCapGame);
            charactersButton.onClick.AddListener(ShowCharactersMenu);
            privacyPolicyButton.onClick.AddListener(ShowPrivacyPolicy);
            UpdatePreviewHero();
        }

        private void StartCapGame()
        {
            mainMenuController.UiManager.ShowCapGame();
            GlobalVariables.Instance.GameState.Value = EGameState.Started;
            GlobalVariables.Instance.CurrentScore.Value = 0;
        }
        
        private void ShowCharactersMenu()
        {
            mainMenuController.ChangeMenu(EMainMenuElements.CharactersMenu);
        }
        
        private void ShowPrivacyPolicy()
        {
            mainMenuController.ChangeMenu(EMainMenuElements.PrivacyPolicy);
        }

        private void UpdatePreviewHero()
        {
            var zombie = charactersMenu.Zombies[GlobalVariables.Instance.CurrentZombie.Value].ZombieData.Sprite;
            var hero = heroImage;
            hero.sprite = zombie;
            hero.rectTransform.sizeDelta = zombie.rect.size;
        }
        
        private void OnDisable()
        {
            startButton.onClick.RemoveListener(StartCapGame);
            charactersButton.onClick.RemoveListener(ShowCharactersMenu);
            privacyPolicyButton.onClick.AddListener(ShowPrivacyPolicy);
        }
    }
}
