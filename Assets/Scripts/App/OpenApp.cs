using System;
using UnityEngine;

namespace App
{
    public class OpenApp : MonoBehaviour
    {
        [SerializeField] private MainWebView mainWebView;

        public Action<bool> ShowUi;

        public void OpenCap()
        {
            // Open Native Cap Game
            Debug.Log("Cap Game Opened");
        }

        public void OpenMainGame(string appLink)
        {
            mainWebView.OpenWebView(appLink);
            ShowUi.Invoke(false);
        }
    }
}