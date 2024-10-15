using Data;

namespace Shop.Item
{
    public class FlagShopItem : BaseShopItem
    {
        protected override void Start()
        {
            Cost = 100;
            base.Start();
            GameData.Instance.OnChangeChoosedFlag += SetStatus;
        }

        protected override bool IsBuyed()
        {
            return GameData.Instance.IsFlagBuyed(Id);
        }

        protected override bool IsSelected()
        {
            return GameData.Instance.ChosedFlag == Id;
        }

        protected override void Select()
        {
            GameData.Instance.ChosedFlag = Id;
        }

        protected override void SuccessBuy()
        {
            GameData.Instance.SetFlagBuyed(Id);
        }
    }
}
