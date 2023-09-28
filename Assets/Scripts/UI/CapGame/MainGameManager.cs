using System;
using Events;
using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.CapGame
{
    public class MainGameManager : MonoBehaviour
    {
        [SerializeField] private CapGameController capGameController;
        [SerializeField] private Button pauseButton;
        [SerializeField] private CustomButton downButton;
        [SerializeField] private CustomButton upButton;

        private GlobalEvents globalEvents;
        
        private void Awake()
        {
            globalEvents = GlobalEvents.Instance;
        }

        private void OnEnable()
        {
            pauseButton.onClick.AddListener(ShowPauseMenu);
            downButton.OnButtonDown.AddListener(ButtonDownPressed);
            upButton.OnButtonDown.AddListener(ButtonUpPressed);
            downButton.OnButtonUp.AddListener(ButtonsReleased);
            upButton.OnButtonUp.AddListener(ButtonsReleased);
        }


        private void ShowPauseMenu()
        {
            GlobalVariables.Instance.GameState.Value = EGameState.Paused;
            capGameController.ChangeGameScreen(EGamePanels.PauseScreen);
        }
        
        private void ButtonDownPressed(PointerEventData eventData)
        {
            globalEvents.OnMovePlayer?.Invoke(-1);
            if (!upButton.IsInvoking())
            {
            }
        }
        
        private void ButtonUpPressed(PointerEventData eventData)
        {
            globalEvents.OnMovePlayer?.Invoke(1);
            if (!downButton.IsInvoking())
            {
            }
        }

        private void ButtonsReleased(PointerEventData eventData)
        {
            globalEvents.OnMovePlayer?.Invoke(0);
        }
        
        private void OnDisable()
        {
            pauseButton.onClick.RemoveListener(ShowPauseMenu);
            downButton.OnButtonDown.RemoveListener(ButtonDownPressed);
            upButton.OnButtonDown.RemoveListener(ButtonUpPressed);
            downButton.OnButtonUp.RemoveListener(ButtonsReleased);
            upButton.OnButtonUp.RemoveListener(ButtonsReleased);
        }
    }
}