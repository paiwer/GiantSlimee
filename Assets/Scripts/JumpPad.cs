using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private string _tagPlayer = "Player";
    [SerializeField] private float _jumpForce;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _tagPlayer)
        {
            GameObject player = other.gameObject;
            _rb = player.GetComponent<Rigidbody>();

            LaunchPlayer();
        }
    }

    private void LaunchPlayer()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _jumpForce, _rb.velocity.z);
    }
}