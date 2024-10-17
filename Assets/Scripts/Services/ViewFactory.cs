using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class ViewFactory : MonoBehaviour
    {
        [SerializeField] private GameObject background;
        [SerializeField] private RectTransform safeArea;

        public string CurrentUrl => uniWebView.Url;

        UniWebView uniWebView;
        int openTabsCount = 1;

        public static ViewFactory Instance;
        private void Awake()
        {
            Instance = this;
        }

        public void CreateWView(string url)
        {
            background.SetActive(true);

            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;

            UniWebView.SetAllowJavaScriptOpenWindow(true);

            uniWebView = gameObject.AddComponent<UniWebView>();
            uniWebView.OnOrientationChanged += (view, orientation) =>
            {
                Invoke("ResizeView", Time.deltaTime);
            };

            uniWebView.SetAcceptThirdPartyCookies(true);

            ResizeView();

            uniWebView.Load(url);
            uniWebView.Show();
            uniWebView.SetAllowBackForwardNavigationGestures(true);
            uniWebView.SetSupportMultipleWindows(true, true);
            uniWebView.OnShouldClose += (view) => view.CanGoBack || openTabsCount > 1;
            uniWebView.OnMultipleWindowOpened += (view, id) => openTabsCount++;
            uniWebView.OnMultipleWindowClosed += (view, id) => openTabsCount--;
        }

        private void ResizeViewSafeArea()
        {
            Rect safeArea = Screen.safeArea;
            if (Screen.width < Screen.height)
            {
                float avg = (2 * safeArea.yMax + Screen.height) / 3;
                this.safeArea.anchorMin = Vector2.zero;
                this.safeArea.anchorMax = new Vector2(1, avg / Screen.height);
            }
            else
            {
                this.safeArea.anchorMin = Vector2.zero;
                this.safeArea.anchorMax = Vector2.one;
            }
            this.safeArea.offsetMin = Vector2.zero;
            this.safeArea.offsetMax = Vector2.zero;
        }

        private void ResizeView()
        {
            ResizeViewSafeArea();
            uniWebView.ReferenceRectTransform = safeArea;
            uniWebView.UpdateFrame();
        }
    }
}
