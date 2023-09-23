using System.Collections;
using System.Collections.Generic;
using App;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private OpenApp openApp;
    
    [Header("UI Elements")]
    [SerializeField] private MainMenuController mainMenuUi;

    public MainMenuController MainMenuUi => mainMenuUi;
    
    private void OnEnable()
    {
        openApp.ShowUi += ShowUI;
    }

    private void ShowUI(bool enabled)
    {
        mainMenuUi.gameObject.SetActive(enabled);
        //splashScreen.gameObject.SetActive(enabled);
    }
    
    private void OnDisable()
    {
        openApp.ShowUi -= ShowUI;
    }
}
