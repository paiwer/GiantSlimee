using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float _mouseSpeedVer = 0f;
    [SerializeField] private float _mouseSpeedHor = 0f;
    [SerializeField] private float _minRotate = 0;
    [SerializeField] private float _maxRotate = 0;

    [SerializeField] private GameObject _camera;
    [SerializeField] private Transform _camPivot;

    private float _rotationX;
    private float _rotationY;
    private float _camRotateY;

    [SerializeField] private float _armLength = 5f;

    [SerializeField] private LayerMask _obstacleLayerMask;

    [SerializeField] private float _minArmLength = 0.6f;
    [SerializeField] private float _sphereCastRadius = 0.3f;

    private RaycastHit _obstacleHit;
    private PlayerInfo _playerInfo;
    private float _changedArmLength;
    private bool _hitObstacle;

    // Start is called before the first frame update
    void Start()
    {
        _playerInfo = GetComponent<PlayerInfo>();
        _changedArmLength = _armLength;
    }

    // Update is called once per frame
    void Update()
    {
        RotateFollowMouse();
        CameraHitObstacleCheck();
    }

    private void RotateFollowMouse()
    {
        _rotationX += Input.GetAxis("Mouse X") * _mouseSpeedHor;
        _rotationY += Input.GetAxis("Mouse Y") * _mouseSpeedVer;

        _camRotateY = Mathf.Clamp(_rotationY, _minRotate, _maxRotate);

        transform.rotation = Quaternion.Euler(0, _rotationX, 0);
        _camPivot.rotation = Quaternion.Euler(-_camRotateY, _rotationX, 0);
    }

    private void CameraHitObstacleCheck()
    {
        float finalArmLength = _armLength * _playerInfo.CurrentSize;

        if (Physics.SphereCast(_camPivot.position, _sphereCastRadius, -_camera.transform.forward, out _obstacleHit, _armLength, _obstacleLayerMask))
        {
            finalArmLength = Mathf.Max(_obstacleHit.distance, _minArmLength);
            _hitObstacle = true;
        }
        else
        {
            _hitObstacle = false;
        }
        _camera.transform.position = _camPivot.position - (_camera.transform.forward * finalArmLength);
    }
}
