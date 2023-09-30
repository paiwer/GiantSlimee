using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDamage : MonoBehaviour
{
    private PlayerInfo _playerInfo;
    private Rigidbody _rigidbody;

    [SerializeField] private float _fallingThreshold;
    [SerializeField] private float _damageMultiplier;
    [SerializeField] private bool _onGround;
    [SerializeField] private bool _takeFallDamage;

    private float _fallDamage;

    public float FallDamage => _fallDamage;
    public bool TakeFallDamage => _takeFallDamage;
    public bool OnGround => _onGround;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody.velocity.y < -_fallingThreshold)
        {
            _fallDamage = Mathf.Abs(_rigidbody.velocity.y + _damageMultiplier);
            _takeFallDamage = true;
        }

        if(_onGround && _takeFallDamage)
        {
            _takeFallDamage = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    { 
        if(collision.gameObject.tag == "Floor")
        {
            _onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _onGround = false;
    }
}
