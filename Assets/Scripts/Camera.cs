using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float _mouseSpeedVer = 0f;
    [SerializeField] private float _mouseSpeedHor = 0f;
    [SerializeField] private float _zoomSpeed = 0;
    [SerializeField] private float _minRotate = 0;
    [SerializeField] private float _maxRotate = 0;
    [SerializeField] private float _minZoom = 0;
    [SerializeField] private float _maxZoom = 0;

    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _camPivot;

    private float _rotationX;
    private float _rotationY;
    private float _camRotateY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateFollowMouse();
        ScrollInOut();
    }

    private void RotateFollowMouse()
    {
        _rotationX += Input.GetAxis("Mouse X") * _mouseSpeedHor;
        _rotationY += Input.GetAxis("Mouse Y") * _mouseSpeedVer;

        _camRotateY = Mathf.Clamp(_rotationY, _minRotate, _maxRotate);

        transform.rotation = Quaternion.Euler(0, _rotationX, 0);
        _camPivot.transform.rotation = Quaternion.Euler(-_camRotateY, _rotationX, 0);
    }

    private void ScrollInOut()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel > 0)    //In
        {
            _camera.transform.position += _camera.transform.forward * _zoomSpeed * Time.deltaTime;
        }
        if (scrollWheel < 0)    //Out
        {
            _camera.transform.position += -_camera.transform.forward * _zoomSpeed * Time.deltaTime;
        }
    }
}
