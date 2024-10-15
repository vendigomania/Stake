using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Trophies
{
    public class TrophyItem : MonoBehaviour
    {
        [SerializeField] private GameObject inactive;
        [SerializeField] private Image img;

        public void UpdateStatus(Sprite _sprite, bool _unlock)
        {
            img.sprite = _sprite;
            inactive.SetActive(!_unlock);
        }
    }
}
