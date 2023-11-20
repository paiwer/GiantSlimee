using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] _sfx;
    [SerializeField] private AudioSource _sfxSource;

    [SerializeField] private AudioClip _jumpSFX;
    [SerializeField] private AudioClip _fallSFX;
    [SerializeField] private AudioClip _eatSFX;
    [SerializeField] private AudioClip _gemSFX;
    [SerializeField] private AudioClip _spitSFX;
    [SerializeField] private AudioClip _winSFX;

    [SerializeField] private Consume _consumeScript;
    [SerializeField] private Movement _movementScript;
    [SerializeField] private WinPlate _winPlateScript;

    // Start is called before the first frame update
    void Start()
    {
        _consumeScript = FindObjectOfType<Consume>();
        _movementScript = FindObjectOfType<Movement>();
        _winPlateScript = FindObjectOfType<WinPlate>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            _sfxSource.clip = _spitSFX;
            _sfxSource.Play();
        }

        if (_movementScript.IsJump)
        {
            _sfxSource.clip = _jumpSFX;
            //_sfxSource.Play();
            _sfxSource.PlayOneShot(_sfxSource.clip);
        }

    }
}

[System.Serializable]
public class Sound
{
    [SerializeField] private string _name;
    [SerializeField] private AudioClip _audioClip;
}

