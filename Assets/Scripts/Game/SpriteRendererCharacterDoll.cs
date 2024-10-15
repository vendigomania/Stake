using UnityEngine;

namespace Game
{
    public class SpriteRendererCharacterDoll : BaseCharacterDoll<SpriteRenderer>
    {
        protected override void SetBody(Sprite _spr)
        {
            body.sprite = _spr;
        }

        protected override void SetLegs(Sprite _spr)
        {
            leg1.sprite = _spr;
            leg2.sprite = _spr;
        }
    }
}
