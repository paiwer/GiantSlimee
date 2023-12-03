using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementDisplay : MonoBehaviour
{
    [SerializeField] private Image _e1Fire;
    [SerializeField] private Image _e1Water;
    [SerializeField] private Image _e1Wind;
    [SerializeField] private Image _e1None;

    [SerializeField] private Image _e2Fire;
    [SerializeField] private Image _e2Water;
    [SerializeField] private Image _e2Wind;
    [SerializeField] private Image _e2None;

    private Consume _consume;

    private ElementInfo _element1;
    private ElementInfo _element2;

    // Start is called before the first frame update
    void Start()
    {
        _consume = FindObjectOfType<Consume>();
    }

    // Update is called once per frame
    void Update()
    {
        _element1 = _consume.ElementInfo1;
        _element2 = _consume.ElementInfo2;

        ElementDisplay();
    }

    private void ElementDisplay()
    {
        if (_element1 != null)
        {
            switch (_element1.Type)
            {
                case ElementInfo.ElementType.Fire:
                    FirstElementSet(false, true, false, false);
                    break;
                case ElementInfo.ElementType.Water:
                    FirstElementSet(false, false, true, false);
                    break;
                case ElementInfo.ElementType.Wind:
                    FirstElementSet(false, false, false, true);
                    break;
            }
        }
        else
        {
            FirstElementSet(true, false, false, false);
        }

        if (_element2 != null)
        {
            switch (_element2.Type)
            {
                case ElementInfo.ElementType.Fire:
                    SecondElementSet(false, true, false, false);
                    break;
                case ElementInfo.ElementType.Water:
                    SecondElementSet(false, false, true, false);
                    break;
                case ElementInfo.ElementType.Wind:
                    SecondElementSet(false, false, false, true);
                    break;
            }
        }
        else
        {
            SecondElementSet(true, false, false, false);
        }
    }

    private void FirstElementSet(bool none, bool fire, bool water, bool wind)
    {
        _e1None.enabled = none;
        _e1Fire.enabled = fire;
        _e1Water.enabled = water;
        _e1Wind.enabled = wind;
    }
    private void SecondElementSet(bool none, bool fire, bool water, bool wind)
    {
        _e2None.enabled = none;
        _e2Fire.enabled = fire;
        _e2Water.enabled = water;
        _e2Wind.enabled = wind;
    }
}
