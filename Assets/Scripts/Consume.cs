using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Consume : MonoBehaviour
{
    [SerializeField] private PlayerInfo _playerInfo;
    [SerializeField] private Ability_Earth _earth;
    [SerializeField] private Ability_Fire _fire;
    [SerializeField] private Ability_Water _water;
    [SerializeField] private Ability_Wind _wind;

    [SerializeField] private bool _isOpenMouth;

    [SerializeField] private GameObject _element1;
    [SerializeField] private ElementInfo _elementInfo1;

    [SerializeField] private GameObject _element2;
    [SerializeField] private ElementInfo _elementInfo2;

    [SerializeField] private float _dropDistance;

    [SerializeField] private float _eatAmount;

    [SerializeField] private List<GameObject> _eatList = new List<GameObject>();

    private GameObject _eatElement;
    private ElementInfo _eatElementInfo;

    public float EatAmount => _eatAmount;

    // Start is called before the first frame update
    void Start()
    {
        _playerInfo = gameObject.GetComponent<PlayerInfo>();
        _earth = GetComponent<Ability_Earth>();
        _fire = GetComponent<Ability_Fire>();
        _water = GetComponent<Ability_Water>();
        _wind = GetComponent<Ability_Wind>();
    }

    // Update is called once per frame
    void Update()
    {
        Eat();
        Spit();
        EatedElementUpdate();
        ElementChange();
    }

    private void Eat()
    {
        _isOpenMouth = Input.GetKey(KeyCode.E);
    }

    private void Spit()
    {
        if (Input.GetKeyDown(KeyCode.Q) && _eatList.Count != 0)
        {
            Vector3 dropPos = transform.position + transform.up * _dropDistance;

            int lastestElementNum = _eatList.Count -1;

            GameObject lastestElement = _eatList[lastestElementNum];
            ElementInfo lastestElementInfo = lastestElement.GetComponent<ElementInfo>();

            lastestElement.SetActive(true);
            lastestElement.transform.position = dropPos;
            _eatList.Remove(lastestElement);

            _eatAmount -= lastestElementInfo.ElementWeight;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(_isOpenMouth && collision.gameObject.tag == "EatAble")
        {
            _eatElement = collision.gameObject;
            _eatElementInfo = _eatElement.GetComponent<ElementInfo>();

            _eatList.Add(_eatElement);
            _eatElement.SetActive(false);

            _eatAmount += _eatElementInfo.ElementWeight;
        }
    }

    private void EatedElementUpdate()
    {
        if(_eatList.Count > 0)
        {
            _element1 = _eatList[0];
            _elementInfo1 = _element1.GetComponent<ElementInfo>();
        }
        else
        {
            _element1 = null;
            _elementInfo1 = null;
        }

        if(_eatList.Count > 1)
        {
            _element2 = _eatList[1];
            _elementInfo2 = _element2.GetComponent<ElementInfo>();
        }
        else
        {
            _element2 = null;
            _elementInfo2 = null;
        }
    }

    private void ElementChange()
    {
        if (_elementInfo1 != null)
        {
            if (_elementInfo1.Type == ElementInfo.ElementType.Earth)
            {
                _earth.enabled = true;
            }
            if (_elementInfo1.Type == ElementInfo.ElementType.Fire)
            {
                _fire.enabled = true;
            }
            if (_elementInfo1.Type == ElementInfo.ElementType.Water)
            {
                _water.enabled = true;
            }
            if (_elementInfo1.Type == ElementInfo.ElementType.Wind)
            {
                _wind.enabled = true;
            }
        }

        if (_elementInfo2 != null)
        {
            if (_elementInfo2.Type == ElementInfo.ElementType.Earth)
            {
                _earth.enabled = true;
            }
            if (_elementInfo2.Type == ElementInfo.ElementType.Fire)
            {
                _fire.enabled = true;
            }
            if (_elementInfo2.Type == ElementInfo.ElementType.Water)
            {
                _water.enabled = true;
            }
            if (_elementInfo2.Type == ElementInfo.ElementType.Wind)
            {
                _wind.enabled = true;
            }
        }

        if (_elementInfo1 == null && _elementInfo2 == null)
        {
            _earth.enabled = false;
            _fire.enabled = false;
            _water.enabled = false;
            _wind.enabled = false;
        }

        if(_elementInfo1 != null && _elementInfo2 == null)
        {
            if (_elementInfo1.Type != ElementInfo.ElementType.Earth)
            {
                _earth.enabled = false;
            }
            if (_elementInfo1.Type != ElementInfo.ElementType.Fire)
            {
                _fire.enabled = false;
            }
            if (_elementInfo1.Type != ElementInfo.ElementType.Water)
            {
                _water.enabled = false;
            }
            if (_elementInfo1.Type != ElementInfo.ElementType.Wind)
            {
                _wind.enabled = false;
            }
        }

        if(_elementInfo1 != null && _elementInfo2 != null)
        {
            if(_elementInfo1.Type != ElementInfo.ElementType.Earth && _elementInfo2.Type != ElementInfo.ElementType.Earth)
            {
                _earth.enabled = false;
            }
            if (_elementInfo1.Type != ElementInfo.ElementType.Fire && _elementInfo2.Type != ElementInfo.ElementType.Fire)
            {
                _fire.enabled = false;
            }
            if (_elementInfo1.Type != ElementInfo.ElementType.Water && _elementInfo2.Type != ElementInfo.ElementType.Water)
            {
                _water.enabled = false;
            }
            if (_elementInfo1.Type != ElementInfo.ElementType.Wind && _elementInfo2.Type != ElementInfo.ElementType.Wind)
            {
                _wind.enabled = false;
            }
        }
    }
}
