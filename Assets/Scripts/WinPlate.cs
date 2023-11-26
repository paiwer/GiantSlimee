using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlate : MonoBehaviour
{
    [SerializeField] private string _tagPlayer = "Player";
    [SerializeField] private int _requireNumber;
    [SerializeField] private float _animSpeed = 1f;
    [SerializeField] private float _distance;
    [SerializeField] private bool _isWin;

    [Header("Sound")]
    [SerializeField] private string _victorySound = "Victory";

    private Consume _consume;

    private Vector3 _originalPosition;
    private Vector3 _downPosition;

    private float _upTime;
    private float _upDelay = 0.5f;

    public bool IsWin => _isWin;

    // Start is called before the first frame update
    void Start()
    {
        _consume = FindObjectOfType<Consume>();
        _originalPosition = transform.position;
        _downPosition = _originalPosition + Vector3.down * _distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _upTime)
        {
            AnimPlate(_originalPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == _tagPlayer)
        {
            if (_consume.GemNumber >= _requireNumber)
            {
                _isWin = true;

                AudioManager.Instance.PlaySFX(_victorySound);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        _upTime = Time.time + _upDelay;
        AnimPlate(_downPosition);
    }

    private void AnimPlate(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, _animSpeed * Time.deltaTime);
    }
}
