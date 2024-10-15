using Data;
using Shop.Item;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ShopScreen : MonoBehaviour
    {
        [SerializeField] private GameObject charactersPanel;
        [SerializeField] private List<CharacterShopItem> characterShopItems = new List<CharacterShopItem>();

        [SerializeField] private GameObject fieldsPanel;
        [SerializeField] private List<FieldShopItem> fieldShopItems = new List<FieldShopItem>();

        [SerializeField] private GameObject flagsPanel;
        [SerializeField] private List<FlagShopItem> flagShopItems = new List<FlagShopItem>();

        [SerializeField] private GameObject ballsPanel;
        [SerializeField] private List<BallShopItem> ballShopItems = new List<BallShopItem>();

        [SerializeField] private TMP_Text coinsLable;

        int showPanelIndex = 0;


        private void Start()
        {
            for (var i = 0; i < GameData.Instance.CharacterBodySprites.Length; i++)
            {
                if (characterShopItems.Count <= i)
                    characterShopItems.Add(Instantiate(characterShopItems[0], characterShopItems[0].transform.parent));

                characterShopItems[i].SetData(GameData.Instance.CharacterBodySprites[i], i);
            }

            for (var i = 0; i < GameData.Instance.FieldShopIcons.Length; i++)
            {
                if (fieldShopItems.Count <= i)
                    fieldShopItems.Add(Instantiate(fieldShopItems[0], fieldShopItems[0].transform.parent));

                fieldShopItems[i].SetData(GameData.Instance.FieldShopIcons[i], i);
            }

            for (var i = 0; i < GameData.Instance.Flags.Length; i++)
            {
                if (flagShopItems.Count <= i)
                    flagShopItems.Add(Instantiate(flagShopItems[0], flagShopItems[0].transform.parent));

                flagShopItems[i].SetData(GameData.Instance.Flags[i], i);
            }

            for (var i = 0; i < GameData.Instance.BallSprites.Length; i++)
            {
                if (ballShopItems.Count <= i)
                    ballShopItems.Add(Instantiate(ballShopItems[0], ballShopItems[0].transform.parent));

                ballShopItems[i].SetData(GameData.Instance.BallSprites[i], i);
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
            SetActivePanel(0);

            SetCoinsText();

            GameData.Instance.OnChangeCoinsCount += SetCoinsText;

            CustomAudioController.Instance.Click();
        }
        public void Hide()
        {
            var scrolls = GetComponentsInChildren<Scrollbar>();
            foreach (var scroll in scrolls) scroll.value = 0f;
            gameObject.SetActive(false);

            GameData.Instance.OnChangeCoinsCount -= SetCoinsText;
        }

        public void NextPanel() => SetActivePanel(++showPanelIndex);

        public void BackPanel() => SetActivePanel(--showPanelIndex);

        private void SetActivePanel(int _index)
        {
            CustomAudioController.Instance.Click();

            _index += 4;
            _index %= 4;

            showPanelIndex = _index;

            charactersPanel.SetActive(_index == 0);
            flagsPanel.SetActive(_index == 1);
            ballsPanel.SetActive(_index == 2);
            fieldsPanel.SetActive(_index == 3);
        }

        private void SetCoinsText()
        {
            coinsLable.text = GameData.Instance.Coins.ToString();
        }
    }
}
