using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class SettingsScreen : MonoBehaviour
    {
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Toggle musicToggle;

        
        void Start()
        {
            soundToggle.onValueChanged.AddListener(SwitchSound);
            musicToggle.onValueChanged.AddListener(SwitchMusic);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            CustomAudioController.Instance.Click();
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void SwitchSound(bool _value)
        {
            CustomAudioController.Instance.SoundIsActive = _value;
            CustomAudioController.Instance.Click();
        }

        private void SwitchMusic(bool _value)
        {
            CustomAudioController.Instance.MusicIsActive = _value;
            CustomAudioController.Instance.Click();
        }
    }
}
