using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Water : MonoBehaviour
{
    private PlayerInfo _playerInfo;
    private Movement _movement;
    private Consume _consume;

    [SerializeField] private float _waterMoveSpeed;
    [SerializeField] private float _waterDamage;
    [SerializeField] private float _timeThreshold;
    [SerializeField] public bool TakeWaterDamage;

    private float _timeCount;

    public float WaterMoveSpeed => _waterMoveSpeed;
    public float WaterDamage => _waterDamage;

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
                TakeWaterDamage = true;
                _timeCount = 0;
            }
        }
    }
}
