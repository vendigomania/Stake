using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trophies
{
    public class TrophiesScreen : MonoBehaviour
    {
        [SerializeField] private TrophyItem[] trophies;

        public void Show()
        {
            gameObject.SetActive(true);

            for (var i = 0; i < trophies.Length; i++)
                trophies[i].UpdateStatus(GameData.Instance.Flags[i + 1], GameData.Instance.IsTrophyActive(i + 1));

            CustomAudioController.Instance.Click();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
