using System;
using System.Collections;
using UnityEngine.Networking;

namespace Services
{
    public static class Connection
    {
        public static IEnumerator IsServerReachable(Action<bool> onResult,
            string server = "https://google.com", int timeout = 5)
        {
            bool result;
            using (var request = UnityWebRequest.Head(server))
            {
                request.timeout = timeout;
                yield return request.SendWebRequest();

                result = IsRequestSucceed(request);
            }

            onResult?.Invoke(result);
        }

        private static bool IsRequestSucceed(UnityWebRequest request) =>
            request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200;
    }
}