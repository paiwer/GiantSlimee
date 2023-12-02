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

    [Header("Sound")]
    [SerializeField] private string _dieSound = "Die";

    private float _healNumber;

    private Vector3 _size;

    private Heal _healScript;
    private FallingDamage _fallScript;
    private Movement _moveScript;
    private Ability_Wind _windAbility;
    private Ability_Fire _fireAbility;
    private Ability_Water _waterAbility;

    bool _playDieSound = true;

    public float CurrentSize => _currectSizeNumber;
    public float JumpForce => _jumpForce;
    public float CurrentJumpForce => _currentJumpForce;
    public float MaxHP => _hp;
    public float CurrentHP => _currentHp;
    public bool IsDead => _isDead;

    // Start is called before the first frame update
    void Start()
    {
        _healScript = GetComponent<Heal>();
        _fallScript = GetComponent<FallingDamage>();
        _moveScript = GetComponent<Movement>();
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
        ElementCheck();
        Heal();

        if (_currentHp <= 0)
        {
            _isDead = true;

            if(_playDieSound)
            {
                AudioManager.Instance.PlaySFX(_dieSound);
                _playDieSound = false;
            }
        }
    }

    private void FixedUpdate()
    {
        TakeFallDamage();
    }

    private void SizeCalculate()
    {
        _eatAmount = GetComponent<Consume>().EatAmount;

        _currectSizeNumber = _eatAmount;

        _size = new Vector3(_currectSizeNumber, _currectSizeNumber, _currectSizeNumber);

        transform.localScale = _size;
    }

    private void TakeFallDamage()
    {
        if (_fallScript.TakeFallDamage && _moveScript.OnGround)
        {
            _fallScript._IsTakeFallDamage(false);
            _currentHp -= _fallScript.FallDamage;
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
                _fireAbility._TakeFireDamage(false);
            }
        }

        if(_waterAbility.enabled)   //update hp -> water effect
        {
            if(_waterAbility.TakeWaterDamage)
            {
                Debug.Log("Take water damage : " + _waterAbility.WaterDamage);
                _currentHp -= _waterAbility.WaterDamage;
                _waterAbility._TakeWaterDamage(false);
            }
        }
    }

    private void Heal()
    {
        if(_healScript.IsHeal)
        {
            _healNumber = _healScript.HealAmount;
            _currentHp += _healNumber;

            if(_currentHp > _hp)    // Prevent _currentHp > max hp
            {
                float difference = _currentHp - _hp;
                _currentHp -= difference;
            }

            _healScript._Heal(false);
        }
    }
}
