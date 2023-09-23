using App;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;

    public GameObject LoadingScreen => loadingScreen;

    public void ShowSplashScreen(bool splash = true)
    {
        loadingScreen.SetActive(splash);
    }
}
