using System.Collections;
using App;
using LunarConsolePlugin;
using Services.RemoteConfig;
using UI;
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
        //TODO Uncomment on Release remoteConfigManager.StartInitRemoteConfig();
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

    //
    // App entry point
    void Start()
    {
        openApp.OpenCapGame();
        //TODO Uncomment on Release StartCoroutine(Initialize());
    }
    
    [ContextMenu("Start App")]
    public void StartApplication()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        // Show SplashScreen
        //uiManager.ShowLoadingScreen();
        yield return new WaitUntil(() => uiManager.LoadingScreenUi.gameObject.activeSelf);
        
        // 1. Check Internet Connection
        if (!Utilities.CheckForInternetConnection())
        {
            Debug.Log("1. No Internet Connection!");
            openApp.OpenCapGame();
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
            openApp.OpenCapGame();
            yield break;
        }
        Debug.Log("2. Has Remote link: " + remoteLink);
        
        // 3. Check offer_id is not null
        workingLinks.CheckOfferId();
        yield return new WaitUntil(() => workingLinks.IsOfferIdCheckComplete);
        if (!workingLinks.IsOfferIdValid)
        {
            Debug.Log("3. Offer_id is null!");
            openApp.OpenCapGame();
            yield break;
        }
        Debug.Log("3. Offer_id is valid = " + workingLinks.IsOfferIdValid);
        
        // Final: open URL WebView game
        Debug.Log("4. Opening linked app with: " + workingLinks.ProductAppUrl);
        openApp.OpenProduct(workingLinks.ProductAppUrl);
    }
}
