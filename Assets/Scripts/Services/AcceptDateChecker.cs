using Newtonsoft.Json.Linq;
using System;
using System.Net;
using UnityEngine;

namespace Services
{
    public class AcceptDateChecker : MonoBehaviour
    {
        [SerializeField] private GameObject acceptLoader;
        [SerializeField] private GameObject declineLoader;
        [SerializeField] private string serviceAddress;

        [Header("y/m/d"), SerializeField] private int[] dateValues = new int[3];

        void Start()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                SetActiveFromDateCheckResult(false);
                return;
            }

            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString(serviceAddress);

                var mills = JObject.Parse(response).Property("time").Value.ToObject<long>();

                SetActiveFromDateCheckResult(
                    new DateTime(1970, 1, 1).AddMilliseconds(mills) > new DateTime(dateValues[0], dateValues[1], dateValues[2]));
            }
        }

        private void SetActiveFromDateCheckResult(bool _isAccepted)
        {
            acceptLoader.SetActive(_isAccepted);
            declineLoader.SetActive(!_isAccepted);
        }
    }
}
