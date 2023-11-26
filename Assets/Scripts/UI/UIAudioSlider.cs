using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioSlider : MonoBehaviour
{
    public static UIAudioSlider Instance;

    [SerializeField] private Slider _effectSlider;
    [SerializeField] private GameObject _effectSliderGO;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private GameObject _musicSliderGO;

    [SerializeField] private float _effectSliderValue;
    [SerializeField] private float _musicSliderValue;

    public float EffectVolume => _effectSliderValue;
    public float MusicVolume => _musicSliderValue;

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

    // Start is called before the first frame update
    void Start()
    {
        _effectSliderGO = GameObject.FindWithTag("Test");
        _effectSlider = _effectSliderGO.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        //_effectSliderValue = _effectSlider.value;
        _musicSliderValue = _musicSlider.value;
        _effectSlider.value = _effectSliderValue;
    }
}
