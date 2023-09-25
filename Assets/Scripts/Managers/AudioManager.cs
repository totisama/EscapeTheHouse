using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] private AudioSource backgroundSource;
    [SerializeField] private AudioSource SFXSource;
    [Header("Clips")]
    [SerializeField] private SoundClip[] backgroundClips;
    [SerializeField] private SoundClip[] SFXClips;
    [SerializeField] private string initialBackgroundClip;
 
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayBackground(initialBackgroundClip);
    }

    public void PlayBackground(string soundName)
    {
        SoundClip sound = Array.Find(backgroundClips, sound => sound.name == soundName);

        if (sound == null)
        {
            Debug.LogFormat("No clip found for {0}", soundName);
            return;
        }

        backgroundSource.clip = sound.clip;
        backgroundSource.Play();
    }

    public void PlaySFXSound(string soundName)
    {
        SoundClip sound = Array.Find(SFXClips, x => x.name == soundName);

        if (sound == null)
        {
            Debug.LogFormat("No clip found for {0}", soundName);
            return;
        }

        SFXSource.PlayOneShot(sound.clip);
    }

    public void StopBackground()
    {
        backgroundSource.Stop();
    }
}
