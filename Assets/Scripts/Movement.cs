using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private PlayerInfo _playerInfo;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _currentMoveSpeed;
    [SerializeField] private bool _jumpAble;
    [SerializeField] private bool _isJump;
    [SerializeField] private bool _isMove;
    [SerializeField] private float _groundCheckDistance = 1;

    [SerializeField] private LayerMask _groundLayer;

    private Ability_Water _waterAbility;

    private Rigidbody _rigidbody;
    public float MoveSpeed => _moveSpeed;
    public bool IsMove => _isMove;
    public bool IsJump => _isJump;
    public bool JumpAble => _jumpAble;


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
        GroundCheck();
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

        if(_rigidbody.velocity.y > 0)   //While up
        {
            _isJump = true;
        }

        if(_rigidbody.velocity.y <= 0)  //While fall
        {
            _isJump = false;
        }
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

    private void GroundCheck()
    {
        float currentDistance = _groundCheckDistance * _playerInfo.CurrentSize;

        RaycastHit hit;
        _jumpAble = Physics.Raycast(transform.position, Vector3.down, out hit, currentDistance, _groundLayer);

        Debug.DrawRay(transform.position, Vector3.down * currentDistance, Color.red);  //Draw line
    }
}
