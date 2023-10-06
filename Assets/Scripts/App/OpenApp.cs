using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace App
{
    public class OpenApp : MonoBehaviour
    {
        [SerializeField] private MainWebView mainWebView;

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
            SetOrientation();
            SceneManager.LoadScene(sceneBuildIndex: 2);
        }
        
        private void SetOrientation()
        {
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
        }
    }
}