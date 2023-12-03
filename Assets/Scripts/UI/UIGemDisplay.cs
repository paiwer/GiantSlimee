using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGemDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _gemText;
    [SerializeField] private string _frontTextFormat = "x ";

    private Consume _consume;

    // Start is called before the first frame update
    void Start()
    {
        _consume = FindObjectOfType<Consume>();
    }

    // Update is called once per frame
    void Update()
    {
        _gemText.text = _frontTextFormat + _consume.GemNumber;
    }
}
