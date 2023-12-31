using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Water : MonoBehaviour
{
    [SerializeField] private float _waterMoveSpeed;
    [SerializeField] private float _waterDamage;
    [SerializeField] private float _timeThreshold;
    [SerializeField] private bool _takeWaterDamage;

    private float _timeCount;

    private Movement _movement;
    private Consume _consume;

    public float WaterMoveSpeed => _waterMoveSpeed;
    public float WaterDamage => _waterDamage;
    public bool TakeWaterDamage => _takeWaterDamage;

    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<Movement>();
        _consume = GetComponent<Consume>();
    }

    // Update is called once per frame
    void Update()
    {
        _waterMoveSpeed = _movement.MoveSpeed * _consume.EatAmount;
        _waterDamage = _consume.EatAmount;

        WaterBadEffect();
    }

    private void WaterBadEffect()
    {
        if(_movement.IsMove)
        {
            _timeCount += Time.deltaTime;

            if(_timeCount >= _timeThreshold)
            {
                _takeWaterDamage = true;
                _timeCount = 0;
            }
        }
    }

    public void _TakeWaterDamage(bool takedamage)
    {
        _takeWaterDamage = takedamage;
    }
}
