using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIAudioSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private Slider _slider;
    [SerializeField] private string _volumeParameter = "Volume";

    [SerializeField] private float _volumeMulti = 20f;

    private void Awake()
    {
        _audioMixer.GetFloat(_volumeParameter, out float currentVolume);
        _slider.SetValueWithoutNotify(Mathf.Pow(10, currentVolume / _volumeMulti));
        _slider.onValueChanged.AddListener(SliderValueChange);
    }

    public void SliderValueChange(float value)
    {
        _audioMixer.SetFloat(_volumeParameter, Mathf.Log10(value) * _volumeMulti);
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(SliderValueChange);
    }
}
