using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Shop.ShopScreen shopScreen;
    [SerializeField] private Trophies.TrophiesScreen trophiesScreen;
    [SerializeField] private Settings.SettingsScreen settingsScreen;

    public void StartGame()
    {
        gameObject.SetActive(false);
        MainController.Instance.StartGame();

        CustomAudioController.Instance.Click();
    }

    public void ShowShop()
    {
        gameObject.SetActive(false);
        shopScreen.Show();
    }

    public void ShowTrophies()
    {
        gameObject.SetActive(false);
        trophiesScreen.Show();
    }

    public void ShowOptions()
    {
        gameObject.SetActive(false);

        settingsScreen.Show();
    }

    public void BackToStart()
    {
        gameObject.SetActive(true);

        shopScreen.Hide();
        trophiesScreen.Hide();
        settingsScreen.Hide();

        CustomAudioController.Instance.Click();
    }
}
