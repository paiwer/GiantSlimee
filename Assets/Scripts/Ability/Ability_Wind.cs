using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Wind : MonoBehaviour
{
    [SerializeField] private float _windJumpForce;

    private Movement _movement;
    private Consume _consume;

    public float WindJumpForce => _windJumpForce;

    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<Movement>();
        _consume = GetComponent<Consume>();
    }

    // Update is called once per frame
    void Update()
    {
        _windJumpForce = _movement.JumpForce * _consume.EatAmount;
    }
}
