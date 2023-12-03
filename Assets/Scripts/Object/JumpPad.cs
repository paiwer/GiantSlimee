using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private string _tagPlayer = "Player";
    [SerializeField] private float _jumpForce;

    [Header("Sound")]
    [SerializeField] private string _jumpPadSound = "JumpPad";

    private Rigidbody _rb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _tagPlayer)
        {
            _rb = other.gameObject.GetComponent<Rigidbody>();

            LaunchPlayer();

            AudioManager.Instance.PlaySFX(_jumpPadSound);
        }
    }

    private void LaunchPlayer()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _jumpForce, _rb.velocity.z);
    }
}
