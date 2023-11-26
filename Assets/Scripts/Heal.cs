using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private HealPad _healPad;
    [SerializeField] private float _healAmount;

    [Header("Sound")]
    [SerializeField] private string _healSound = "Heal";

    private bool _isHeal;

    public float HealAmount => _healAmount;
    public bool IsHeal => _isHeal;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        _healPad = collision.gameObject.GetComponent<HealPad>();

        if(_healPad != null)
        {
            _healAmount = _healPad.HealAmount;
            _Heal(true);

            AudioManager.Instance.PlaySFX(_healSound);
        }
    }

    public void _Heal(bool isHeal)
    {
        _isHeal = isHeal;
    }
}
