using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] PlayerInfo _playerInfo;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _currentMoveSpeed;
    [SerializeField] private bool _jumpAble;
    [SerializeField] private bool _isMove;
    [SerializeField] private string _tagFloor = "Floor";

    private Ability_Water _waterAbility;

    private Rigidbody _rigidbody;
    public float MoveSpeed => _moveSpeed;
    public bool IsMove => _isMove;

    // Start is called before the first frame update
    void Start()
    {
       _rigidbody = gameObject.GetComponent<Rigidbody>();
       _waterAbility = GetComponent<Ability_Water>();

       _currentMoveSpeed = _moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        ElementCheck();
    }

    private void Move()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        if (horizontalMove > 0)   //D
        {
            transform.position += transform.right * _currentMoveSpeed * Time.deltaTime;
        }
        if(horizontalMove < 0)  //A
        {
            transform.position += -transform.right * _currentMoveSpeed * Time.deltaTime;
        }
        if(verticalMove > 0)    //W
        {
            transform.position += transform.forward * _currentMoveSpeed * Time.deltaTime;
        }
        if(verticalMove < 0)    //S
        {
            transform.position += -transform.forward * _currentMoveSpeed * Time.deltaTime;
        }

        if(horizontalMove != 0 || verticalMove != 0)
        {
            _isMove = true;
        }
        else
        {
            _isMove = false;
        }
    }

    private void Jump()
    {
        if(Input.GetKey(KeyCode.Space) && _jumpAble)
        {
            _rigidbody.velocity = transform.up * _playerInfo.CurrentJumpForce;
            _jumpAble = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        _jumpAble = collision.gameObject.tag == _tagFloor;
    }

    private void OnCollisionExit(Collision collision)
    {
        _jumpAble = false;
    }

    private void ElementCheck()
    {
        if(_waterAbility.enabled)
        {
            _currentMoveSpeed = _waterAbility.WaterMoveSpeed;
        }
        else
        {
            _currentMoveSpeed = _moveSpeed;
        }
    }
}
