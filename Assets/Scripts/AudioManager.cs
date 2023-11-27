using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] _sfxSound, _musicSound;
    [SerializeField] private AudioSource _sfxSource, _musicSource;

    public AudioSource SfxSource => _sfxSource;
    public AudioSource MusicSource => _musicSource;

    [Header("Sound")]
    [SerializeField] private string _music01 = "BG01";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(_music01);
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(_sfxSound, x=> x.Name == name);

        if(sound == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            _sfxSource.PlayOneShot(sound.AudioClip);
        }
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(_musicSound, x => x.Name == name);

        if (sound == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            _musicSource.clip = sound.AudioClip;
            _musicSource.Play();
        }
    }
}

[System.Serializable]
public class Sound
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _audioClip;

    public string Name => _name;
    public AudioClip AudioClip => _audioClip;
}

