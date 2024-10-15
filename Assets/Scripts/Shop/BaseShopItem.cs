using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop.Item
{
    public abstract class BaseShopItem : MonoBehaviour
    {
        [SerializeField] private Button btn;
        [SerializeField] private Image image;
        [SerializeField] protected GameObject costContainer;
        [SerializeField] protected TMP_Text costLable;

        [SerializeField] protected TMP_Text statusText;

        protected int Cost;
        protected int Id;

        protected virtual void Start()
        {
            btn.onClick.AddListener(OnClick);
            costLable.text = Cost.ToString();
        }

        public void SetData(Sprite _icon, int _Id)
        {
            Id = _Id;
            image.sprite = _icon;

            SetStatus();
        }

        private void OnClick()
        {
            if(IsBuyed())
            {
                Select();
                CustomAudioController.Instance.Click();
            }
            else if(GameData.Instance.Coins >= Cost)
            {
                CustomAudioController.Instance.Click();
                GameData.Instance.Coins -= Cost;
                SuccessBuy();
            }

            SetStatus();
        }

        protected void SetStatus()
        {
            costContainer.gameObject.SetActive(!IsBuyed());
            statusText.gameObject.SetActive(IsBuyed());

            statusText.text = IsSelected() ? "Equiped" : "Unequiped";
        }

        protected abstract void Select();
        protected abstract void SuccessBuy();
        protected abstract bool IsBuyed();
        protected abstract bool IsSelected();
    }
}
