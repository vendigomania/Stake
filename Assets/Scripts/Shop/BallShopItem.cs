using Data;

namespace Shop.Item
{
    public class BallShopItem : BaseShopItem
    {
        protected override void Start()
        {
            Cost = 50;
            base.Start();
            GameData.Instance.OnChangeChoosedBall += SetStatus;
        }

        protected override bool IsBuyed()
        {
            return GameData.Instance.IsBallBuyed(Id);
        }

        protected override bool IsSelected()
        {
            return GameData.Instance.ChosedBall == Id;
        }

        protected override void Select()
        {
            GameData.Instance.ChosedBall = Id;
        }

        protected override void SuccessBuy()
        {
            GameData.Instance.SetBallBuyed(Id);
        }
    }
}
