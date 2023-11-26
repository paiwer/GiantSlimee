using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Wind : MonoBehaviour
{
    private PlayerInfo _playerInfo;
    private Consume _consume;
    [SerializeField] private float _windJumpForce;

    public float WindJumpForce => _windJumpForce;

    // Start is called before the first frame update
    void Start()
    {
        _playerInfo = GetComponent<PlayerInfo>();
        _consume = GetComponent<Consume>();
    }

    // Update is called once per frame
    void Update()
    {
        _windJumpForce = _playerInfo.JumpForce * _consume.EatAmount;
    }
}
