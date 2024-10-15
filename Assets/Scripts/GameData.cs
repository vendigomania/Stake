using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Data
{
    public class GameData : MonoBehaviour
    {
        [SerializeField] private bool WipeAllData;

        [SerializeField] private Sprite[] flags;


        public Sprite[] Flags => flags;


        public int EnemyFlagId = 0;
        public int EnemySkinId = 0;

        public static GameData Instance { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            if (WipeAllData) PlayerPrefs.DeleteAll();

            Instance = this;
        }

        #region trophies

        public bool IsTrophyActive(int _id)
        {
            return PlayerPrefs.GetInt("Trophy" + _id, 0) == 1;
        }

        public void SetTrophyActive(int _id)
        {
            PlayerPrefs.SetInt("Trophy" + _id, 1);
        }

        #endregion

        #region shop

        public Sprite[] FieldShopIcons;
        public Sprite[] FieldSprites;

        public Sprite[] CharacterBodySprites;
        public Sprite[] CharacterLegSprites;

        public Sprite[] BallSprites;

        public UnityAction OnChangeCoinsCount;
        public int Coins
        {
            get => PlayerPrefs.GetInt("Coins", 0);
            set
            {
                PlayerPrefs.SetInt("Coins", value);
                OnChangeCoinsCount?.Invoke();
            }
        }

        public UnityAction OnChangeChoosedFlag;
        public int ChosedFlag
        {
            get => PlayerPrefs.GetInt("ChosedFlag", 0);
            set {
                PlayerPrefs.SetInt("ChosedFlag", value);
                OnChangeChoosedFlag?.Invoke();
            }
        }

        public UnityAction OnChangeChoosedBall;
        public int ChosedBall
        {
            get => PlayerPrefs.GetInt("ChosedBall", 0);
            set {
                PlayerPrefs.SetInt("ChosedBall", value);
                OnChangeChoosedBall?.Invoke();
            }
        }

        public UnityAction OnChangeChoosedCharacter;
        public int ChosedCharacter
        {
            get => PlayerPrefs.GetInt("ChosedCharacter", 0);
            set {
                PlayerPrefs.SetInt("ChosedCharacter", value);
                OnChangeChoosedCharacter?.Invoke();
            }
        }

        public UnityAction OnChangeChoosedField;
        public int ChosedField
        {
            get => PlayerPrefs.GetInt("ChosedField", 0);
            set {
                PlayerPrefs.SetInt("ChosedField", value);
                OnChangeChoosedField?.Invoke();
            }
        }

        //Buying
        //Ball
        public bool IsBallBuyed(int _id)
        {
            return PlayerPrefs.GetInt("BallBuyed" + _id, 0) == 1 || _id == 0;
        }

        public void SetBallBuyed(int _id)
        {
            PlayerPrefs.SetInt("BallBuyed" + _id, 1);
        }

        //Flag
        public bool IsFlagBuyed(int _id)
        {
            return PlayerPrefs.GetInt("FlagBuyed" + _id, 0) == 1 || _id == 0;
        }

        public void SetFlagBuyed(int _id)
        {
            PlayerPrefs.SetInt("FlagBuyed" + _id, 1);
        }

        //Field
        public bool IsFieldBuyed(int _id)
        {
            return PlayerPrefs.GetInt("FieldBuyed" + _id, 0) == 1 || _id == 0;
        }

        public void SetFieldBuyed(int _id)
        {
            PlayerPrefs.SetInt("FieldBuyed" + _id, 1);
        }

        //Character
        public bool IsCharacterBuyed(int _id)
        {
            return PlayerPrefs.GetInt("CharacterBuyed" + _id, 0) == 1 || _id == 0;
        }

        public void SetCharacterBuyed(int _id)
        {
            PlayerPrefs.SetInt("CharacterBuyed" + _id, 1);
        }

        #endregion
    }
}
