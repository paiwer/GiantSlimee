using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementInfo : MonoBehaviour
{
    public enum ElementType
    {
        Fire,
        Water,
        Wind,
        None,
    }
    public ElementType Type;

    [SerializeField] private float _weight;
    public float ElementWeight => _weight;

    void Start()
    {
        transform.localScale = new Vector3(_weight, _weight, _weight);
    }
}
