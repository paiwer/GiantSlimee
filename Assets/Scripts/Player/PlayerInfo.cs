using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private float _hp = 0;
    [SerializeField] private float _currentHp = 0;

    [SerializeField] private float _currectSize = 0;
    [SerializeField] private bool _isDead;

    [Header("Sound")]
    [SerializeField] private string _dieSound = "Die";

    private float _eatAmount;
    private float _healNumber;

    private bool _playDieSound = true;

    private Vector3 _size;

    private Heal _healScript;
    private FallingDamage _fallScript;
    private Movement _moveScript;

    private Ability_Fire _fireAbility;
    private Ability_Water _waterAbility;

    public float CurrentSize => _currectSize;
    public float MaxHP => _hp;
    public float CurrentHP => _currentHp;
    public bool IsDead => _isDead;

    // Start is called before the first frame update
    void Start()
    {
        _healScript = GetComponent<Heal>();
        _fallScript = GetComponent<FallingDamage>();
        _moveScript = GetComponent<Movement>();
        _fireAbility = GetComponent<Ability_Fire>();
        _waterAbility = GetComponent<Ability_Water>();

        _currentHp = _hp;
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

    void FixedUpdate()
    {
        TakeFallDamage();
    }

    private void SizeCalculate()
    {
        _eatAmount = GetComponent<Consume>().EatAmount;

        _currectSize = _eatAmount;

        _size = new Vector3(_currectSize, _currectSize, _currectSize);

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
