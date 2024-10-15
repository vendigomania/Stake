using Data;
using UnityEngine;

namespace Game
{
    public abstract class BaseCharacterDoll<T> : MonoBehaviour
    {
        [SerializeField] protected T body;
        [SerializeField] protected T leg1;
        [SerializeField] protected T leg2;

        public void SetCharacterSkin(int _id)
        {
            SetBody(GameData.Instance.CharacterBodySprites[_id]);
            SetLegs(GameData.Instance.CharacterLegSprites[_id]);
        }

        protected abstract void SetBody(Sprite _spr);

        protected abstract void SetLegs(Sprite _spr);
    }
}
