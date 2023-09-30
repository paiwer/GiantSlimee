using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private float _hp = 0;
    [SerializeField] private float _currentHp = 0;
    [SerializeField] private float _jumpForce = 0;
    [SerializeField] private float _currentJumpForce = 0;
    [SerializeField] private float _standardSize = 1;
    [SerializeField] private float _currectSizeNumber = 0;
    [SerializeField] private float _minSize;
    [SerializeField] private float _eatAmount;
    [SerializeField] private bool _isDead;

    private Vector3 _size;

    private FallingDamage _fallScript;
    private Ability_Wind _windAbility;
    private Ability_Fire _fireAbility;
    private Ability_Water _waterAbility;

    public float CurrentSize => _currectSizeNumber;
    public float JumpForce => _jumpForce;
    public float CurrentJumpForce => _currentJumpForce;

    // Start is called before the first frame update
    void Start()
    {
        _fallScript = GetComponent<FallingDamage>();
        _windAbility = GetComponent<Ability_Wind>();
        _fireAbility = GetComponent<Ability_Fire>();
        _waterAbility = GetComponent<Ability_Water>();

        _currentHp = _hp;
        _currentJumpForce = _jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        SizeCalculate();
        TakeFallDamage();
        ElementCheck();

        if (_currentHp <= 0)
        {
            _isDead = true;
            Debug.Log("Dead");
        }
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

    private void ElementCheck()
    {
        if(_windAbility.enabled)    //update jump force -> wind effect
        {
            _currentJumpForce = _windAbility.WindJumpForce;
        }
        else
        {
            _currentJumpForce = _jumpForce;
        }

        if(_fireAbility.enabled)    //update hp -> fire effect
        {
            if(_fireAbility.BurnObject)
            {
                Debug.Log("Take fire damage : " + _fireAbility.FireDamage);
                _currentHp -= _fireAbility.FireDamage;
                _fireAbility.BurnObject = false;
            }
        }

        if(_waterAbility.enabled)   //update hp -> water effect
        {
            if(_waterAbility.TakeWaterDamage)
            {
                Debug.Log("Take water damage : " + _waterAbility.WaterDamage);
                _currentHp -= _waterAbility.WaterDamage;
                _waterAbility.TakeWaterDamage = false;
            }
        }
    }
}
