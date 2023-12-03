using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private string _healSound = "Heal";

    private bool _isHeal;
    private float _healAmount;

    private HealPad _healPad;

    public float HealAmount => _healAmount;
    public bool IsHeal => _isHeal;

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
