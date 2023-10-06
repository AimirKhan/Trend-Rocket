using System.Collections;
using App;
using Services.RemoteConfig;
using UI.MainMenu;
using UI.SplashScreen;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private OpenApp openApp;
    [SerializeField] private WorkingLinks workingLinks;

    [Header("Services")]
    [SerializeField] private RemoteConfigManager remoteConfigManager;
    [SerializeField] private string oneSignalAppId;

    [SerializeField] private PrivacyPolicy privacyPolicy;
    [SerializeField] private LoadingScreen loadingScreen;

    private void Awake()
    {
        // 1. Remote Config Manager
        //TODO Uncomment on Release remoteConfigManager.StartInitRemoteConfig();
#if !DEBUG
        // 3. OneSignal
        if (oneSignalAppId != "")
        {
            //OneSignal.Initialize(oneSignalAppId);
        }
#endif

        // Last. Set 60fps
        Application.targetFrameRate = 60;
    }

    //
    // App entry point
    void Start()
    {
        //openApp.OpenCapGame(); // Comment to Full app working
        StartCoroutine(Initialize()); //TODO Uncomment on Full app working
    }

    private IEnumerator Initialize()
    {
        yield return ShowPrivacyPolicy();

        yield return ShowSplashScreen();
        /*
        // 3. Check Internet Connection
        if (!Utilities.CheckForInternetConnection())
        {
            Debug.Log("1. No Internet Connection!");
            openApp.OpenCapGame();
            yield break;
        }
        Debug.Log("1. Has Internet Connection!");
        */

        yield return CheckRemoteConfig();

        yield return CheckOfferId();
        
        StartProduct();
    }

    private IEnumerator ShowPrivacyPolicy()
    {
        privacyPolicy.Init();
        yield return new WaitUntil(() => privacyPolicy.IsPrivacyPolicyAccepted);
    }

    private IEnumerator ShowSplashScreen()
    {
        loadingScreen.gameObject.SetActive(true);
        yield return new WaitUntil(() => loadingScreen.gameObject.activeSelf);
    }

    private IEnumerator CheckRemoteConfig()
    {
        yield return remoteConfigManager.Init();
        yield return new WaitUntil(() => workingLinks.IsRemoteConfigFetched);
        workingLinks.GetAppLinks();
        yield return new WaitUntil(() => workingLinks.IsLinksGets);
        var remoteLinkExist = workingLinks.IsLinkExist;
        if (!remoteLinkExist)
        {
            Debug.Log("2. Remote link is null!");
            openApp.OpenCapGame();
            yield break;
        }
        Debug.Log("2. Has Remote link: " + remoteLinkExist);
    }

    private IEnumerator CheckOfferId()
    {
        workingLinks.CheckOfferId();
        yield return new WaitUntil(() => workingLinks.IsOfferIdCheckComplete);
        if (!workingLinks.IsOfferIdValid)
        {
            Debug.Log("3. Offer_id is null!");
            openApp.OpenCapGame();
            yield break;
        }
        Debug.Log("3. Offer_id is valid = " + workingLinks.IsOfferIdValid);
    }

    private void StartProduct()
    {
        Debug.Log("4. Opening linked app with: " + workingLinks.ProductAppUrl);
        openApp.OpenProduct(workingLinks.ProductAppUrl);
        loadingScreen.gameObject.SetActive(false);
    }
}
