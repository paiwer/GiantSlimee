using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] PlayerInfo _playerInfo;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _jumpAble;

    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
       _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _jumpForce = _playerInfo.JumpForce;

        Move();
        Jump();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * _moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.forward * _moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * _moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -transform.right * _moveSpeed * Time.deltaTime;
        }
    }

    private void Jump()
    {
        if(Input.GetKey(KeyCode.Space) && _jumpAble)
        {
            _rigidbody.velocity = transform.up * _jumpForce;
            _jumpAble = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            _jumpAble = true;
        }
    }
}
