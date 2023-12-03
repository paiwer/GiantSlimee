using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimControls : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    private Movement _movement;
    private string _moveParameter = "Move";

    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_movement.IsMove && !_movement.IsJump)
        {
            _anim.SetBool(_moveParameter, true);
        }
        else
        {
            _anim.SetBool(_moveParameter, false);
        }
    }
}
