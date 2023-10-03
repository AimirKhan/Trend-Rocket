using System.Collections.Generic;
using Gpm.WebView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Services.WebView
{
    public class CapGPMWebView : MonoBehaviour
    {
        private const string DEFAULT_URL = "https://www.google.com";

        public string urlInput;
        public string titleInput;
        public string customSchemeInput;

        public string backgroundColorInput;
        public string navigationColorInput;

        public bool clearCookieToggle;
        public bool clearCacheToggle;

        public bool isNavigationBarVisible;
        public bool isBackButtonVisible;
        public bool isForwardButtonVisible;
        public bool isCloseButtonVisible;

        public bool isSupportMultipleWindow;
        public bool isBackButtonCloseCallbackUsed;
        public bool maskViewVisibleToggle;
        public bool autoRotationToggle;

        public int styleDropdown;
        public int orientationDropdown;
        public Dropdown contentModeDropdown;

        public string popupXInput;
        public string popupYInput;
        public string popupWidthInput;
        public string popupHeightInput;
        public string popupMarginsLeftInput;
        public string popupMarginsTopInput;
        public string popupMarginsRightInput;
        public string popupMarginsBottomInput;

        public string userAgentStringInput;
        public string javascriptInput;

        public string safeBrowsingNavigationBarColor;
        public string safeBrowsingNavigationTextColor;

        public string output;

        private void Awake()
        {
            urlInput = DEFAULT_URL;

            backgroundColorInput = "#FFFFFF";
            navigationColorInput = "#4B96E6";
            safeBrowsingNavigationBarColor = "#4B96E6";
            safeBrowsingNavigationTextColor = "#FFFFFF";

#if UNITY_ANDROID
            maskViewVisibleToggle = false;
            autoRotationToggle = false;
#endif
        }

        public void OpenWebView(string url)
        {
            urlInput = url;
            if (string.IsNullOrEmpty(urlInput) == false)
            {
                GpmWebView.ShowUrl(urlInput, GetConfiguration(), OnWebViewCallback, GetCustomSchemeList());
            }
            else
            {
                Debug.LogError("[SampleWebView] Input url is empty.");
            }
        }

        private GpmWebViewRequest.Configuration GetConfiguration()
        {
            GpmWebViewRequest.CustomSchemePostCommand customSchemePostCommand = new GpmWebViewRequest.CustomSchemePostCommand();
            customSchemePostCommand.Close("CUSTOM_SCHEME_POST_CLOSE");

            return new GpmWebViewRequest.Configuration()
            {
                style = styleDropdown,
                orientation = (orientationDropdown == 0) ? GpmOrientation.UNSPECIFIED : 1 << (orientationDropdown - 1),
                isClearCache = clearCookieToggle,
                isClearCookie = clearCacheToggle,
                backgroundColor = backgroundColorInput,
                isNavigationBarVisible = isNavigationBarVisible,
                navigationBarColor = navigationColorInput,
                title = titleInput,
                isBackButtonVisible = isBackButtonVisible,
                isForwardButtonVisible = isForwardButtonVisible,
                isCloseButtonVisible = isCloseButtonVisible,
                supportMultipleWindows = isSupportMultipleWindow,
                userAgentString = userAgentStringInput,
                addJavascript = javascriptInput,
                customSchemePostCommand = customSchemePostCommand,

                position = GetConfigurationPosition(),
                size = GetConfigurationSize(),
                margins = GetConfigurationMargins(),

                isBackButtonCloseCallbackUsed = isBackButtonCloseCallbackUsed,

#if UNITY_IOS
            contentMode = contentModeDropdown.value,
            isMaskViewVisible = maskViewVisibleToggle.isOn,
            isAutoRotation = autoRotationToggle.isOn
#endif
            };
        }

        private GpmWebViewRequest.Position GetConfigurationPosition()
        {
            bool hasValue = false;
            if (string.IsNullOrEmpty(popupXInput) == false && string.IsNullOrEmpty(popupYInput) == false)
            {
                hasValue = true;
            }

            int x = 0;
            int.TryParse(popupXInput, out x);

            int y = 0;
            int.TryParse(popupYInput, out y);

            return new GpmWebViewRequest.Position
            {
                hasValue = hasValue,
                x = x,
                y = y
            };
        }

        private GpmWebViewRequest.Size GetConfigurationSize()
        {
            bool hasValue = false;
            if (string.IsNullOrEmpty(popupWidthInput) == false && string.IsNullOrEmpty(popupHeightInput) == false)
            {
                hasValue = true;
            }

            int width = 0;
            int.TryParse(popupWidthInput, out width);

            int height = 0;
            int.TryParse(popupHeightInput, out height);

            return new GpmWebViewRequest.Size
            {
                hasValue = hasValue,
                width = width,
                height = height
            };
        }

        private GpmWebViewRequest.Margins GetConfigurationMargins()
        {
            bool hasValue = false;
            if (string.IsNullOrEmpty(popupMarginsLeftInput) == false &&
                string.IsNullOrEmpty(popupMarginsTopInput) == false &&
                string.IsNullOrEmpty(popupMarginsRightInput) == false &&
                string.IsNullOrEmpty(popupMarginsBottomInput) == false)
            {
                hasValue = true;
            }

            int marginLeft = 0;
            int.TryParse(popupMarginsLeftInput, out marginLeft);

            int marginTop = 0;
            int.TryParse(popupMarginsTopInput, out marginTop);

            int marginRight = 0;
            int.TryParse(popupMarginsRightInput, out marginRight);

            int marginBottom = 0;
            int.TryParse(popupMarginsBottomInput, out marginBottom);

            return new GpmWebViewRequest.Margins
            {
                hasValue = hasValue,
                left = marginLeft,
                top = marginTop,
                right = marginRight,
                bottom = marginBottom
            };
        }

        private List<string> GetCustomSchemeList()
        {
            List<string> customSchemeList = null;
            if (string.IsNullOrEmpty(customSchemeInput) == false)
            {
                string[] schemes = customSchemeInput.Split(',');
                customSchemeList = new List<string>(schemes);
            }
            return customSchemeList;
        }

        private void OnWebViewCallback(GpmWebViewCallback.CallbackType callbackType, string data, GpmWebViewError error)
        {
            Debug.Log("OnWebViewCallback: " + callbackType);
            switch (callbackType)
            {
                case GpmWebViewCallback.CallbackType.Open:
                    if (error != null)
                    {
                        Debug.LogFormat("Fail to open WebView. Error:{0}", error);
                    }
                    break;
                case GpmWebViewCallback.CallbackType.Close:
                    if (error != null)
                    {
                        Debug.LogFormat("Fail to close WebView. Error:{0}", error);
                    }
                    break;
                case GpmWebViewCallback.CallbackType.PageStarted:
                    if (string.IsNullOrEmpty(data) == false)
                    {
                        Debug.LogFormat("PageStarted Url : {0}", data);
                    }
                    break;
                case GpmWebViewCallback.CallbackType.PageLoad:
                    if (string.IsNullOrEmpty(data) == false)
                    {
                        Debug.LogFormat("Loaded Page:{0}", data);
                    }
                    break;
                case GpmWebViewCallback.CallbackType.MultiWindowOpen:
                    Debug.Log("MultiWindowOpen");
                    break;
                case GpmWebViewCallback.CallbackType.MultiWindowClose:
                    Debug.Log("MultiWindowClose");
                    break;
                case GpmWebViewCallback.CallbackType.Scheme:
                    Debug.LogFormat("Scheme:{0}", data);
                    break;
                case GpmWebViewCallback.CallbackType.GoBack:
                    Debug.Log("GoBack");
                    break;
                case GpmWebViewCallback.CallbackType.GoForward:
                    Debug.Log("GoForward");
                    break;
                case GpmWebViewCallback.CallbackType.ExecuteJavascript:
                    Debug.LogFormat("ExecuteJavascript data : {0}, error : {1}", data, error);
                    break;
#if UNITY_ANDROID
                case GpmWebViewCallback.CallbackType.BackButtonClose:
                    Debug.Log("BackButtonClose");
                    break;
#endif
            }
        }

        public void CanGoBack()
        {
            bool value = GpmWebView.CanGoBack();
            output = value.ToString();
        }

        public void GoBack()
        {
            GpmWebView.GoBack();
        }

        public void CanGoForward()
        {
            bool value = GpmWebView.CanGoForward();
            output = value.ToString();
        }

        public void GoForward()
        {
            GpmWebView.GoForward();
        }

        public void GetX()
        {
            int value = GpmWebView.GetX();
            output = value.ToString();
        }

        public void GetY()
        {
            int value = GpmWebView.GetY();
            output = value.ToString();
        }

        public void GetWidth()
        {
            int value = GpmWebView.GetWidth();
            output = value.ToString();
        }

        public void GetHeight()
        {
            int value = GpmWebView.GetHeight();
            output = value.ToString();
        }

        public void ExecuteJavascript()
        {
            GpmWebView.ExecuteJavaScript(javascriptInput);
        }

        public void IsActive()
        {
            bool value = GpmWebView.IsActive();
            output = value.ToString();
        }

        public void Close()
        {
            GpmWebView.Close();
        }

        public void SetPosition()
        {
            int x = 0;
            int y = 0;
            int.TryParse(popupXInput, out x);
            int.TryParse(popupYInput, out y);
            GpmWebView.SetPosition(x, y);
        }

        public void SetSize()
        {
            int width = 0;
            int height = 0;
            int.TryParse(popupWidthInput, out width);
            int.TryParse(popupHeightInput, out height);
            GpmWebView.SetSize(width, height);
        }

        public void SetMargins()
        {
            int left = 0;
            int top = 0;
            int right = 0;
            int bottom = 0;
            int.TryParse(popupMarginsLeftInput, out left);
            int.TryParse(popupMarginsTopInput, out top);
            int.TryParse(popupMarginsRightInput, out right);
            int.TryParse(popupMarginsBottomInput, out bottom);
            GpmWebView.SetMargins(left, top, right, bottom);
        }

        public void ResetPosition()
        {
            popupXInput = string.Empty;
            popupYInput = string.Empty;
        }

        public void ResetSize()
        {
            popupWidthInput = string.Empty;
            popupHeightInput = string.Empty;
        }

        public void ResetMargins()
        {
            popupMarginsLeftInput = string.Empty;
            popupMarginsTopInput = string.Empty;
            popupMarginsRightInput = string.Empty;
            popupMarginsBottomInput = string.Empty;
        }
    }
}
