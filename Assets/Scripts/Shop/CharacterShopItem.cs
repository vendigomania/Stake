using Data;

namespace Shop.Item
{
    public class CharacterShopItem : BaseShopItem
    {
        protected override void Start()
        {
            Cost = 200;
            base.Start();
            GameData.Instance.OnChangeChoosedCharacter += SetStatus;
        }

        protected override bool IsBuyed()
        {
            return GameData.Instance.IsCharacterBuyed(Id);
        }

        protected override bool IsSelected()
        {
            return GameData.Instance.ChosedCharacter == Id;
        }

        protected override void Select()
        {
            GameData.Instance.ChosedCharacter = Id;
        }

        protected override void SuccessBuy()
        {
            GameData.Instance.SetCharacterBuyed(Id);
        }
    }
}
