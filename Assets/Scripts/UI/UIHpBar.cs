using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpBar : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;

    [SerializeField] private float _animSpeed;
    [SerializeField] private float _animDelay;

    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _healBar;
    [SerializeField] private Image _damageBar;

    private PlayerInfo _playerInfo;

    private float _currentFillAmount;
    private float _timer;

    private float _previousHp;

    // Start is called before the first frame update
    void Start()
    {
        _playerInfo = FindObjectOfType<PlayerInfo>();

        _maxHealth = _playerInfo.MaxHP;

        _currentFillAmount = _currentHealth / _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        _currentHealth = _playerInfo.CurrentHP;

        if (_currentHealth != _previousHp) //Hp change
        {
            if(_currentHealth > _previousHp)    //Increase
            {
                FillAmountCalculate();

                SetAnimTimer();
                SetTakeDamageFalse();

                HPBarIncrease();
            }

            if(_currentHealth < _previousHp)    //Reduce
            {
                FillAmountCalculate();

                SetAnimTimer();
                SetTakeDamageFalse();

                HPBarDecrease();
            }
        }

        HealthBarAnim();
    }

    private void FillAmountCalculate()
    {
        _currentFillAmount = _currentHealth / _maxHealth;
    }

    private void SetAnimTimer()
    {
        _timer = Time.time + _animDelay;
    }

    private void SetTakeDamageFalse()
    {
        _previousHp = _currentHealth;
    }

    private void HPBarIncrease()
    {
        if (_healBar.fillAmount == _damageBar.fillAmount)    //Normal increase
        {
            _healBar.fillAmount = _currentFillAmount;
            _damageBar.fillAmount = _currentFillAmount;
        }
        if (_hpBar.fillAmount != _damageBar.fillAmount)    //Increase While take damage or Increase then take damage
        {
            _healBar.fillAmount = _currentFillAmount;
        }
    }

    private void HPBarDecrease()
    {
        if (_hpBar.fillAmount == _healBar.fillAmount)    //Normal decrease
        {
            _hpBar.fillAmount = _currentFillAmount;
            _healBar.fillAmount = _currentFillAmount;
        }
        if (_hpBar.fillAmount != _damageBar.fillAmount)  //Decrease While heal
        {
            _healBar.fillAmount = _currentFillAmount;
        }
    }

    private void HealthBarAnim()
    {
        if (Time.time > _timer)
        {
            if (_hpBar.fillAmount != _currentFillAmount)
            {
                SetBarFillAmount(_hpBar);
            }
            if (_healBar.fillAmount != _currentFillAmount)
            {
                SetBarFillAmount(_healBar);
            }
            if (_damageBar.fillAmount != _currentFillAmount)
            {
                SetBarFillAmount(_damageBar);
            }
        }
    }

    private void SetBarFillAmount(Image bar)
    {
        bar.fillAmount = Mathf.MoveTowards(bar.fillAmount, _currentFillAmount, _animSpeed * Time.deltaTime);
    }
}
