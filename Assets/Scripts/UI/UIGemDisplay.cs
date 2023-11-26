using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGemDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _gemText;
    [SerializeField] private string _form = "x ";

    private Consume _consumeScript;

    // Start is called before the first frame update
    void Start()
    {
        _consumeScript = FindObjectOfType<Consume>();
    }

    // Update is called once per frame
    void Update()
    {
        _gemText.text = _form + _consumeScript.GemNumber;
    }
}
