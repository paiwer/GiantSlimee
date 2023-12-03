using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISfxTest : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private string _testSfx = "Victory";

    public void PlaySFXSound()
    {
        AudioManager.Instance.PlaySFX(_testSfx);
    }
}
