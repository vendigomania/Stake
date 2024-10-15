using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private VersusScreen versusScreen;
        [SerializeField] private ResultScreen resultScreen;

        [SerializeField] private GameObject noticeScreen;

        [SerializeField] private KickDragScreen dragScreen;

        [SerializeField] private Button backBtn;
        [SerializeField] private TMP_Text triesLable;
        [SerializeField] private TMP_Text coinsLable;

        [SerializeField] private Image fieldRenderer;

        [Header("Ingame")]
        [SerializeField] private Transform launchPlatform;
        [SerializeField] private SpriteRendererCharacterDoll playerDoll;
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private SpriteRenderer ballRenderer;
        [SerializeField] private Rigidbody2D ball;

        [SerializeField] private SpriteRendererCharacterDoll[] enemiesLine1;
        [SerializeField] private SpriteRendererCharacterDoll[] enemiesLine2;

        private const int BaseTriesCount = 7;
        private int RemainBalls;
        private int Gools;

        float ballObserveDelay;

        // Start is called before the first frame update
        void Start()
        {
            dragScreen.OnDragProcess += OnDragProcess;
            dragScreen.OnDragEnd += OnDragEnd;

            backBtn.onClick.AddListener(BackToMenu);

            KeeperController.OnKeepBall += OnKeepBall;
            GoolTrigger.OnGoooal += Goool;
        }

        private void Update()
        {
            if (ballObserveDelay == 0) return;

            if (ballObserveDelay > 1)
            {
                ballObserveDelay -= Time.deltaTime;
                return;
            }

            if (ball.velocity.magnitude < 0.5f) OnKeepBall();
        }

        public void StartGame()
        {
            GameData.Instance.EnemyFlagId = Random.Range(1, GameData.Instance.Flags.Length);
            while (GameData.Instance.EnemyFlagId == 0 || GameData.Instance.EnemyFlagId == GameData.Instance.ChosedFlag)
            {
                GameData.Instance.EnemyFlagId = ++GameData.Instance.EnemyFlagId % GameData.Instance.Flags.Length;
            }

            GameData.Instance.EnemySkinId = Random.Range(0, GameData.Instance.CharacterBodySprites.Length);
            if (GameData.Instance.EnemySkinId == GameData.Instance.ChosedCharacter)
                GameData.Instance.EnemySkinId = ++GameData.Instance.EnemySkinId % GameData.Instance.CharacterBodySprites.Length;

            resultScreen.Hide();
            versusScreen.Show();
            ballRenderer.sprite = GameData.Instance.BallSprites[GameData.Instance.ChosedBall];
            fieldRenderer.sprite = GameData.Instance.FieldSprites[GameData.Instance.ChosedField];

            playerDoll.SetCharacterSkin(GameData.Instance.ChosedCharacter);


            foreach (var enemy in enemiesLine1)
                enemy.SetCharacterSkin(GameData.Instance.EnemySkinId);

            foreach (var enemy in enemiesLine2)
                enemy.SetCharacterSkin(GameData.Instance.EnemySkinId);

            RemainBalls = BaseTriesCount;
            Gools = 0;

            triesLable.text = RemainBalls.ToString();
            coinsLable.text = GameData.Instance.Coins.ToString();

            ReloadField();

            CustomAudioController.Instance.Click();
        }

        public void BackToMenu()
        {
            ReloadField();
            MainController.Instance.BackToMenu();
        }

        private void ReloadField()
        {
            dragScreen.enabled = true;

            ball.gameObject.SetActive(true);
            ball.velocity = Vector3.zero;
            ball.position = Vector2.up * -2;

            ballObserveDelay = 0f;

            launchPlatform.rotation = Quaternion.identity;
        }

        private void OnDragProcess(Vector2 delta)
        {
            delta = Vector3.Normalize(delta);
            launchPlatform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.up, delta));
        }

        private void OnDragEnd(Vector2 delta)
        {
            dragScreen.enabled = false;

            ball.AddForce(delta);

            ballObserveDelay = 3f;

            RemainBalls--;
            triesLable.text = RemainBalls.ToString();
        }

        private void OnKeepBall()
        {
            ReloadField();

            CustomAudioController.Instance.Miss();

            CheckResult();
        }

        private void Goool()
        {
            ReloadField();

            CustomAudioController.Instance.Goal();
            Gools++;

            CheckResult();
        }

        private void CheckResult()
        {
            if(RemainBalls <= 0)
            {
                resultScreen.Show(Gools);

                if(BaseTriesCount/2 < Gools) GameData.Instance.SetTrophyActive(GameData.Instance.EnemyFlagId);
            }
        }
    }
}
