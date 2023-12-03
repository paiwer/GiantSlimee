using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _currentMoveSpeed;
    [SerializeField] private bool _onGround;
    [SerializeField] private bool _onJumpPad;
    [SerializeField] private bool _isJump;
    [SerializeField] private bool _isMove;
    [SerializeField] private float _groundCheckLength = 1;

    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _jumpPadLayer;

    [Header("Sound")]
    [SerializeField] private string _jumpSound = "Jump";

    private Ability_Water _waterAbility;
    private PlayerInfo _playerInfo;
    private Rigidbody _rigidbody;

    public float MoveSpeed => _moveSpeed;
    public bool IsMove => _isMove;
    public bool IsJump => _isJump;
    public bool OnGround => _onGround;
    public bool OnJumpPad => _onJumpPad;


    // Start is called before the first frame update
    void Start()
    {
       _rigidbody = gameObject.GetComponent<Rigidbody>();
       _waterAbility = GetComponent<Ability_Water>();
       _playerInfo = GetComponent<PlayerInfo>();

        _currentMoveSpeed = _moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();

        GroundCheck();
        ElementCheck();
    }

    void FixedUpdate()
    {
        Move();
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
        if(Input.GetKeyDown(KeyCode.Space) && _onGround)
        {
            _rigidbody.velocity = transform.up * _playerInfo.CurrentJumpForce;

            AudioManager.Instance.PlaySFX(_jumpSound);
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
        float currentDistance = _groundCheckLength * _playerInfo.CurrentSize;

        RaycastHit groundHit;
        _onGround = RaycastFromBody(out groundHit, currentDistance, _groundLayer);

        RaycastHit jumpPadHit;
        _onJumpPad = RaycastFromBody(out jumpPadHit, currentDistance,_jumpPadLayer);

        Debug.DrawRay(transform.position, Vector3.down * currentDistance, Color.red);  //Draw line
    }

    private bool RaycastFromBody(out RaycastHit hitInfo, float distance, LayerMask layerMask)
    {
        return Physics.Raycast(transform.position, Vector3.down, out hitInfo, distance, layerMask);
    }
}
