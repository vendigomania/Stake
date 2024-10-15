using Data;
using Game;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private StartScreen startScreen;

    public static MainController Instance;

    private void Start()
    {
        Instance = this;
    }

    public void StartGame()
    {
        startScreen.gameObject.SetActive(false);
        gameController.StartGame();
    }

    public void BackToMenu()
    {
        startScreen.BackToStart();
    }
}
