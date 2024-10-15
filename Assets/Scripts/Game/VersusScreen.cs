using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class VersusScreen : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [Space, SerializeField] private Image enemyFlag;
        [SerializeField] private ImageCharacterDoll enemyCharacter;

        [Space, SerializeField] private Image playerFlag;
        [SerializeField] private ImageCharacterDoll playerCharacter;

        [SerializeField] private Image ball;
        
        public void Show()
        {
            enemyFlag.sprite = GameData.Instance.Flags[GameData.Instance.EnemyFlagId];
            enemyCharacter.SetCharacterSkin(GameData.Instance.EnemySkinId);

            playerFlag.sprite = GameData.Instance.Flags[GameData.Instance.ChosedFlag];
            playerCharacter.SetCharacterSkin(GameData.Instance.ChosedCharacter);

            ball.sprite = GameData.Instance.BallSprites[GameData.Instance.ChosedBall];

            animator.Play("Show");
        }
    }
}
