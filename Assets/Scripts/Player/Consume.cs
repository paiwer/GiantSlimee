using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Consume : MonoBehaviour
{
    [SerializeField] private bool _isOpenMouth;

    [SerializeField] private GameObject _element1;
    [SerializeField] private ElementInfo _elementInfo1;

    [SerializeField] private GameObject _element2;
    [SerializeField] private ElementInfo _elementInfo2;

    [SerializeField] private float _dropDistance;

    [SerializeField] private float _currentEatAmount = 1f;

    [SerializeField] private int _gemNumber = 0;

    [SerializeField] private List<GameObject> _eatList = new List<GameObject>();

    [SerializeField] private string _tagEatable = "EatAble";

    [Header("Sound")]
    [SerializeField] private string _eatSound = "Eat";
    [SerializeField] private string _eatGemSound = "EatGem";
    [SerializeField] private string _splitSound = "Split";

    private GameObject _eatElement;

    private ElementInfo _eatElementInfo;
    private Ability_Fire _fire;
    private Ability_Water _water;
    private Ability_Wind _wind;

    public ElementInfo ElementInfo1 => _elementInfo1;
    public ElementInfo ElementInfo2 => _elementInfo2;
    public float EatAmount => _currentEatAmount;
    public int GemNumber => _gemNumber;

    // Start is called before the first frame update
    void Start()
    {
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

            _currentEatAmount -= lastestElementInfo.ElementWeight;

            AudioManager.Instance.PlaySFX(_splitSound);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(_isOpenMouth && collision.gameObject.tag == _tagEatable)
        {
            _eatElement = collision.gameObject;
            _eatElementInfo = _eatElement.GetComponent<ElementInfo>();

            EatAmountUpdate();

            _eatElement.SetActive(false);
        }
    }

    private void EatedElementUpdate()
    {
        if(_eatList.Count > 0)  //Have item in list = 1 or more
        {
            _element1 = _eatList[0];
            _elementInfo1 = _element1.GetComponent<ElementInfo>();
        }
        else
        {
            _element1 = null;
            _elementInfo1 = null;
        }

        if(_eatList.Count > 1)  //Have item in list = 2 or more
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
        ElementSet();

        if (_elementInfo1 == null && _elementInfo2 == null)
        {
            DisableAllElement();
        }
    }

    private void DisableAllElement()
    {
        _fire.enabled = false;
        _water.enabled = false;
        _wind.enabled = false;
    }

    private void ElementSet()
    {
        if (_elementInfo2 != null)      //Have first and second
        {
            switch (_elementInfo2.Type)
            {
                case ElementInfo.ElementType.Fire:
                    _fire.enabled = true;
                    break;
                case ElementInfo.ElementType.Water:
                    _water.enabled = true;
                    break;
                case ElementInfo.ElementType.Wind:
                    _wind.enabled = true;
                    break;
            }
        }

        if(_elementInfo1 != null && _elementInfo2 == null)  //Have first but no second
        {
            switch (_elementInfo1.Type)
            {
                case ElementInfo.ElementType.Fire:
                    ElementControl(true, false, false);
                    break;
                case ElementInfo.ElementType.Water:
                    ElementControl(false, true, false);
                    break;
                case ElementInfo.ElementType.Wind:
                    ElementControl(false, false, true);
                    break;
            }
        }
    }

    private void ElementControl(bool fire, bool water, bool wind)
    {
        _fire.enabled = fire;
        _water.enabled = water;
        _wind.enabled = wind;
    }

    private void EatAmountUpdate()
    {
        if (_eatElementInfo.Type != ElementInfo.ElementType.None)
        {
            _eatList.Add(_eatElement);
            _currentEatAmount += _eatElementInfo.ElementWeight;

            AudioManager.Instance.PlaySFX(_eatSound);
        }

        if (_eatElementInfo.Type == ElementInfo.ElementType.None)
        {
            _gemNumber += 1;

            AudioManager.Instance.PlaySFX(_eatGemSound);
        }
    }
}
