using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Fire : MonoBehaviour
{
    [SerializeField] private float _fireDamage;
    [SerializeField] private bool _burnObject;
    [SerializeField] private string _tagBurnable = "Burnable";

    private Consume _consume;

    [Header("Sound")]
    [SerializeField] private string _burnSound = "Burn";

    public float FireDamage => _fireDamage;
    public bool BurnObject => _burnObject;

    // Start is called before the first frame update
    void Start()
    {
        _consume = GetComponent<Consume>();
    }

    // Update is called once per frame
    void Update()
    {
        _fireDamage = _consume.EatAmount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.enabled)
        {
            if (collision.gameObject.tag == _tagBurnable)
            {
                _burnObject = true;
                collision.gameObject.SetActive(false);

                AudioManager.Instance.PlaySFX(_burnSound);
            }
        }
    }

    public void _TakeFireDamage(bool takeDamage)
    {
        _burnObject = takeDamage;
    }
}
