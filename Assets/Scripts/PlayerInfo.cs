using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private float _hp = 0;
    [SerializeField] private float _currentHp = 0;
    [SerializeField] private float _jumpForce = 0;
    [SerializeField] private float _fallingDamage = 0;
    [SerializeField] private float _startHeight = 0;
    [SerializeField] private float _standardSize = 1;
    [SerializeField] private float _currectSizeNumber = 0;
    [SerializeField] private float _minSize;
    [SerializeField] private float _eatAmount;

    private Vector3 _size;

    private FallingDamage _fallScript;

    public float CurrentSize => _currectSizeNumber;
    public float JumpForce => _jumpForce;
    public float FallingDamage => _fallingDamage;

    // Start is called before the first frame update
    void Start()
    {
        _fallScript = GetComponent<FallingDamage>();
        _currentHp = _hp;
    }

    // Update is called once per frame
    void Update()
    {
        SizeCalculate();
        TakeFallDamage();
    }

    private void FixedUpdate()
    {

    }

    private void SizeCalculate()
    {
        _eatAmount = GetComponent<Consume>().EatAmount;

        if(_eatAmount == 0)
        {
            _currectSizeNumber = _standardSize;
        }
        else
        {
            _currectSizeNumber = _eatAmount + 1;
        }
        _size = new Vector3(_currectSizeNumber, _currectSizeNumber, _currectSizeNumber);

        transform.localScale = _size;
    }

    private void TakeFallDamage()
    {
        if(_fallScript.TakeFallDamage && _fallScript.OnGround)
        {
            _currentHp -= _fallScript.FallDamage;
            Debug.Log("HP : " + _currentHp + " / " + _hp);
        }
    }

    private void Dead()
    {
        if(_currentHp <= 0)
        {

        }
    }

}
