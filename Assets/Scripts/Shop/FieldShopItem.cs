using Data;

namespace Shop.Item
{
    public class FieldShopItem : BaseShopItem
    {
        protected override void Start()
        {
            Cost = 400;
            base.Start();
            GameData.Instance.OnChangeChoosedField += SetStatus;
        }

        protected override bool IsBuyed()
        {
            return GameData.Instance.IsFieldBuyed(Id);
        }

        protected override bool IsSelected()
        {
            return GameData.Instance.ChosedField == Id;
        }

        protected override void Select()
        {
            GameData.Instance.ChosedField = Id;
        }

        protected override void SuccessBuy()
        {
            GameData.Instance.SetFieldBuyed(Id);
        }
    }
}
