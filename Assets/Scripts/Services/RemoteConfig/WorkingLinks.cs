using System;
using System.Collections;
using Unity.Services.RemoteConfig;
using UnityEngine;
using UnityEngine.Networking;

namespace Services.RemoteConfig
{
    public class WorkingLinks : MonoBehaviour
    {
        [SerializeField] private string remoteConfigKey = "remote_link";
        [SerializeField] private string cropLinkKeyword = "click_api";
        
        public string RemoteUrl { get; private set; }
        public string ProductAppUrl { get; private set; }
        public bool IsRemoteConfigFetched { get; private set; }
        public bool IsLinksGets { get; private set; }
        public bool IsOfferIdCheckComplete { get; private set; }
        public bool IsOfferIdValid { get; private set; }
        
        private void OnEnable()
        {
            RemoteConfigService.Instance.FetchCompleted += (ctx => IsRemoteConfigFetched = true);
        }

        public void GetAppLinks()
        {
            if (IsLinksGets)
                return;
            RemoteUrl = RemoteConfigService.Instance.appConfig.GetString(remoteConfigKey);
            
            var keywordIndex = RemoteUrl.IndexOf(cropLinkKeyword, StringComparison.Ordinal);
            var result = RemoteUrl.Substring(
                0, RemoteUrl.Length - cropLinkKeyword.Length - keywordIndex - 1);
            ProductAppUrl = result;
            IsLinksGets = true;
        }

        public void CheckOfferId()
        {
            if (IsOfferIdCheckComplete)
                return;
            StartCoroutine(StartCheckOfferId());
        }
        
        private IEnumerator StartCheckOfferId()
        {
            if (IsOfferIdCheckComplete)
                yield break;
            
            var request = UnityWebRequest.Get(RemoteUrl);
            yield return request.SendWebRequest();
            var response = JsonUtility.FromJson<Response>(request.downloadHandler.text);

            IsOfferIdValid = response.info.offer_id != "";
            IsOfferIdCheckComplete = true;
        }
        
        private void OnDisable()
        {
            RemoteConfigService.Instance.FetchCompleted += (ctx => IsRemoteConfigFetched = true);
        }
    }
}