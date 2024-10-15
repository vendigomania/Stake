using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource clickSource;
    [SerializeField] private AudioSource goalSource;
    [SerializeField] private AudioSource missSource;
    [SerializeField] private AudioSource victorySource;

    [SerializeField, Header("Music")] private AudioSource musicSource;

    public static CustomAudioController Instance { get; private set; }

    public bool SoundIsActive = true;

    public bool MusicIsActive
    {
        get => !musicSource.mute;
        set => musicSource.mute = !value;
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void Click()
    {
        if (SoundIsActive) clickSource.Play();
    }

    public void Goal()
    {
        if (SoundIsActive) goalSource.Play();
    }

    public void Miss()
    {
        if (SoundIsActive) missSource.Play();
    }

    public void Victory()
    {
        if (SoundIsActive) victorySource.Play();
    }
}
