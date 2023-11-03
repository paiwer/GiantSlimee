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
    [SerializeField] private string _tagFloor = "Floor";

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

        if(_takeFallDamage && _rigidbody.velocity.y > 0)    //case keep jump on jump pad
        {
            _takeFallDamage = false;
        }

        if(_onGround && _takeFallDamage)    //Take damage
        {
            _takeFallDamage = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    { 
        if(collision.gameObject.tag == _tagFloor)
        {
            _onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _onGround = false;
    }
}
