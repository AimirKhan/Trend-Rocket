using System;
using Events;
using UI.CapGame;
using UnityEngine;
using UnityEngine.UI;

namespace UI.FinalScreen
{
    public class FinalScreenManager : MonoBehaviour
    {
        [SerializeField] private CapGameController capGameController;
        [SerializeField] private Image heroImage;
        [SerializeField] private Button okayButton;

        private void OnEnable()
        {
            okayButton.onClick.AddListener(BackToMainMenu);
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
        
        private void OnDisable()
        {
            okayButton.onClick.RemoveListener(BackToMainMenu);
        }
    }
}
