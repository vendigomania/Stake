using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ResultScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleLable;
        [SerializeField] private TMP_Text coinsText;
        [SerializeField] private TMP_Text rewardText;
        [SerializeField] private Image playerFlag;

        [SerializeField] private GameObject winState;
        [SerializeField] private GameObject loseState;

        [SerializeField] private Button Again;
        [SerializeField] private Button Menu;

        private void Start()
        {
            Again.onClick.AddListener(() =>
            {
                MainController.Instance.StartGame();
                Hide();
            });
            Menu.onClick.AddListener(() =>
            {
                MainController.Instance.BackToMenu();
                Hide();
            });
        }

        public void Show(int gools)
        {
            bool _isWin = gools > 0;

            if(_isWin) CustomAudioController.Instance.Victory();
            else CustomAudioController.Instance.Miss();

            playerFlag.sprite = GameData.Instance.Flags[GameData.Instance.ChosedFlag];
            coinsText.text = GameData.Instance.Coins.ToString();

            if(_isWin) GameData.Instance.Coins += gools * 10;

            rewardText.text = string.Format("+{0}", gools * 10);
            titleLable.text = _isWin ? "You win!" : "You lose!";

            gameObject.SetActive(true);

            rewardText.gameObject.SetActive(_isWin);
            winState.SetActive(_isWin);
            loseState.SetActive(!_isWin);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
