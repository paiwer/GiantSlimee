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

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(_weight, _weight, _weight);
    }

    // Update is called once per frame
    void Update()
    {

    }
}