using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class ViewFactory : MonoBehaviour
    {
        [SerializeField] private GameObject background;
        [SerializeField] private RectTransform safeScreenArea;

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
            UniWebView.SetAllowJavaScriptOpenWindow(true);

            Screen.orientation = ScreenOrientation.AutoRotation;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;

            background.SetActive(true);

            uniWebView = gameObject.AddComponent<UniWebView>();
            uniWebView.OnOrientationChanged += (view, orientation) =>
            {
                StartCoroutine(UpdateScreen());
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

        private void UpdateSafeArea()
        {
            Rect _safeArea = Screen.safeArea;
            
            if (Screen.width < Screen.height)
            {
                float avg = (2 * _safeArea.yMax + Screen.height) / 3;
                
                safeScreenArea.anchorMax = new Vector2(1, avg / Screen.height);
            }
            else
            {
                safeScreenArea.anchorMax = Vector2.one;
            }

            safeScreenArea.anchorMin = Vector2.zero;

            safeScreenArea.offsetMin = Vector2.zero;
            safeScreenArea.offsetMax = Vector2.zero;
        }

        IEnumerator UpdateScreen()
        {
            yield return null;
            UpdateSafeArea();
            uniWebView.ReferenceRectTransform = safeScreenArea;
            uniWebView.UpdateFrame();
        }
    }
}
