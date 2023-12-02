using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDamage : MonoBehaviour
{
    private Movement _movementScript;

    [SerializeField] private float _fallingThreshold = 12;
    [SerializeField] private float _damageMultiplier = 5;
    [SerializeField] private bool _takeFallDamage;

    private Rigidbody _rigidbody;

    [SerializeField] private float _fallDamage;

    public float FallDamage => _fallDamage;
    public bool TakeFallDamage => _takeFallDamage;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _movementScript = GetComponent<Movement>();
    }

    private void FixedUpdate()
    {
        if (_rigidbody.velocity.y < -_fallingThreshold)
        {
            _fallDamage = Mathf.Abs(_rigidbody.velocity.y + _damageMultiplier);
            _takeFallDamage = true;
        }

        if (_takeFallDamage && _movementScript.OnJumpPad)    //case keep jump on jump pad
        {
            _takeFallDamage = false;
        }
    }


    public void _IsTakeFallDamage(bool isTakeDamage)
    {
        _takeFallDamage = isTakeDamage;
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);     //Set velocity y = 0, to prevent take double damage
    }
}
