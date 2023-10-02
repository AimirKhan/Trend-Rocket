using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class MainWebView : MonoBehaviour
{
    WebViewObject webViewObject;

    public void OpenWebView(string url)
    {
        StartCoroutine(StartWebView(url));
    }
    
    IEnumerator StartWebView(string url) {
        webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
        webViewObject.Init(
            (msg) =>
            {
                Debug.Log(msg);
            },
            zoom: false);

        // Centrally located
        webViewObject.SetMargins(0, 0, 0, 0);
        webViewObject.SetTextZoom(100);
        webViewObject.SetScrollbarsVisibility(false);
        webViewObject.SetVisibility(true);
        
        if (url.StartsWith("http"))
        {
            webViewObject.LoadURL(url.Replace(" ", "%20"));
        }
        else
        {
            var srcArchive = Path.Combine(Application.streamingAssetsPath, url);
            var cachePath = Path.Combine(Application.temporaryCachePath, url);
            var newArchive = cachePath + ".zip";
            Debug.Log("Local URL path: " + srcArchive);
            Debug.Log("Data path : " + cachePath);
            if (!File.Exists(newArchive))
            {
                var unityWebRequest = UnityWebRequest.Get(srcArchive);
                yield return unityWebRequest.SendWebRequest();
                var result = unityWebRequest.downloadHandler.data;
                File.WriteAllBytes(newArchive, result);
                System.IO.Compression.ZipFile.ExtractToDirectory(newArchive, cachePath);
            }
            var newLocalUrl = cachePath + "/index.html";
            Debug.Log("index.html path : " + newLocalUrl);
            Debug.Log("File exist? " + File.Exists(newLocalUrl));
            
            webViewObject.LoadURL("file://" + newLocalUrl.Replace(" ", "%20"));
        }
    }
}
