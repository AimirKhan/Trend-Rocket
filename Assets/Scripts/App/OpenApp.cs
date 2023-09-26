using System;
using Game;
using UnityEngine;
using UnityEngine.Events;

namespace App
{
    public class OpenApp : MonoBehaviour
    {
        [SerializeField] private MainWebView mainWebView;

        public Action HideNative;
        public UnityEvent OnOpenCap;

        public void OpenCapGame()
        {
            // Open Native Cap Game
            mainWebView.gameObject.SetActive(false);
            OnOpenCap.Invoke();
        }

        public void OpenProduct(string appLink)
        {
            mainWebView.OpenWebView(appLink);
            HideNative.Invoke();
        }
    }
}