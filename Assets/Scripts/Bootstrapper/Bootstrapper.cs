using System.Collections;
using App;
using LunarConsolePlugin;
using Services.RemoteConfig;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private OpenApp openApp;
    [SerializeField] private WorkingLinks workingLinks;

    [Header("Services")]
    [SerializeField] private RemoteConfigManager remoteConfigManager;
    [SerializeField] private LunarConsole lunarConsole;
    [SerializeField] private string oneSignalAppId;
    
    [SerializeField] private UiManager uiManager;

    private void Awake()
    {
        // 1. Remote Config Manager
        remoteConfigManager.StartInitRemoteConfig();
#if DEVELOPMENT_BUILD
        // 2. Lunar console
        lunarConsole?.gameObject.SetActive(true);

#endif
#if !DEBUG
        // 3. OneSignal
        if (oneSignalAppId != "")
        {
            OneSignal.Initialize(oneSignalAppId);
        }
#endif

    }

    void Start()
    {
        StartCoroutine(Initialize());
    }
    
    [ContextMenu("Start App")]
    public void StartApplication()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        // Show SplashScreen
        uiManager.MainMenuUi.ShowSplashScreen();
        yield return new WaitUntil(() => uiManager.MainMenuUi.LoadingScreen.activeSelf);
        
        // 1. Check Internet Connection
        if (!Utilities.CheckForInternetConnection())
        {
            Debug.Log("1. No Internet Connection!");
            openApp.OpenCap();
            yield break;
        }
        Debug.Log("1. Has Internet Connection!");
        
        // 2. Check RemoteConfig URL is not null
        yield return new WaitUntil(() => workingLinks.IsRemoteConfigFetched);
        workingLinks.GetAppLinks();
        yield return new WaitUntil(() => workingLinks.IsLinksGets);
        var remoteLink = workingLinks.RemoteUrl;
        if (!remoteLink.StartsWith("http"))
        {
            Debug.Log("2. Remote link is null!");
            openApp.OpenCap();
            yield break;
        }
        Debug.Log("2. Has Remote link: " + remoteLink);
        
        // 3. Check offer_id is not null
        workingLinks.CheckOfferId();
        yield return new WaitUntil(() => workingLinks.IsOfferIdCheckComplete);
        if (!workingLinks.IsOfferIdValid)
        {
            Debug.Log("3. Offer_id is null!");
            openApp.OpenCap();
            yield break;
        }
        Debug.Log("3. Offer_id is valid = " + workingLinks.IsOfferIdValid);
        
        // Final: open URL WebView game
        Debug.Log("4. Opening linked app with: " + workingLinks.ProductAppUrl);
        openApp.OpenMainGame(workingLinks.ProductAppUrl);
    }
}
