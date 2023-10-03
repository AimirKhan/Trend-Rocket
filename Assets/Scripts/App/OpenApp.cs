using Services.WebView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace App
{
    public class OpenApp : MonoBehaviour
    {
        //[SerializeField] private MainWebView mainWebView;
        //[SerializeField] private CapGPMWebView capGpmWebView;
        [SerializeField] private CapGPMWebview2 capGpmWebview2;

        public UnityEvent OnOpenCap;

        public void OpenCapGame()
        {
            // Open Native Cap Game
            // Load CapGameScene
            //OnOpenCap.Invoke();
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }

        public void OpenProduct(string appLink)
        {
            //mainWebView.OpenWebView(appLink);
            //capGpmWebView.OpenWebView(appLink);
            capGpmWebview2.ShowUrlFullScreen(appLink);
            
            SetOrientation();
        }
        
        private void SetOrientation()
        {
            Screen.autorotateToPortrait = true;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }
}