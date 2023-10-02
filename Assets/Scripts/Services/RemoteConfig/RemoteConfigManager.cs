using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;
using UnityEngine.Networking;

namespace Services.RemoteConfig
{
    public class RemoteConfigManager : MonoBehaviour
    {
        public struct userAttributes {}
        public struct appAttributes {}
    
        async Task InitializeRemoteConfigAsync()
        {
            await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }

        public IEnumerator Init()
        {
            var isServerReachable = false;
            yield return Connection.IsServerReachable(ctx => isServerReachable = ctx);
            if (isServerReachable)
            {
                var task = InitializeRemoteConfigAsync();
                yield return new WaitUntil(() => task.IsCompleted);
            }

            RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
            RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
        }

        void ApplyRemoteSettings(ConfigResponse configResponse)
        {
            //Debug.Log("RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config);
        }
    }
}