﻿using Events;
using UI.CapGame;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainGame
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private CapGameController capGameController;
        [SerializeField] private Image heroImage;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button backButton;

        private void OnEnable()
        {
            backButton.onClick.AddListener(BackToMainMenu);
            continueButton.onClick.AddListener(ContinueGame);
            UpdatePreviewHero();
        }

        private void UpdatePreviewHero()
        {
            var zombie = GlobalVariables.Instance.zombies
                [GlobalVariables.Instance.CurrentZombie.Value].Sprite;
            var hero = heroImage;
            hero.sprite = zombie;
            hero.rectTransform.sizeDelta = zombie.rect.size;
        }

        private void BackToMainMenu()
        {
            capGameController.GoToMainMenu();
            GlobalVariables.Instance.CurrentScore.Value = 0;
            GlobalEvents.Instance.OnGameState.Invoke(EGameState.Stopped);
        }

        private void ContinueGame()
        {
            capGameController.ChangeGameScreen();
            GlobalEvents.Instance.OnGameState.Invoke(EGameState.Continued);
        }
        
        private void OnDisable()
        {
            backButton.onClick.RemoveListener(BackToMainMenu);
            continueButton.onClick.RemoveListener(ContinueGame);
        }
    }
}