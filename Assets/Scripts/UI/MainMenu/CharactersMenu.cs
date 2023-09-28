using System.Collections.Generic;
using Game;
using Services;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class CharactersMenu : MonoBehaviour
    {
        private MainMenuController mainMenuController;
        [SerializeField] private List<Character> zombies;
        [SerializeField] private Button backButton;
        [SerializeField] private float charactersSpriteScale = 1.0f;

        public List<Character> Zombies => zombies;

        private string selectedText = "Selected";
        private bool IsCharactersInit;
        
        private void Awake()
        {
            mainMenuController = transform.parent.gameObject.GetComponent<MainMenuController>();
            GlobalVariables.Instance.CurrentZombie
                .Subscribe(UpdateCharactersText)
                .AddTo(this);
        }
        
        private void OnEnable()
        {
            backButton.onClick.AddListener(BackToMainMenu);
        }

        private void BackToMainMenu()
        {
            mainMenuController.ChangeMenu(EMainMenuElements.MainMenu);
        }

        private void UpdateCharactersText(int selected)
        {
            foreach (var zombie in zombies)
            {
                if (!IsCharactersInit)
                {
                    zombie.PreviewScale = charactersSpriteScale;
                }
                zombie.ButtonText = zombie.ZombieData.AccessibleTypes.ValueText;
                if (selected == zombie.ZombieData.ZombieId)
                {
                    zombie.ButtonText = selectedText;
                }
            }
        }
        
        private void OnDisable()
        {
            backButton.onClick.RemoveListener(BackToMainMenu);
        }
    }
}
