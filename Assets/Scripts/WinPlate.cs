using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlate : MonoBehaviour
{
    [SerializeField] private string _tagPlayer = "Player";
    [SerializeField] private float _requireSize;

    private Consume _consume;

    // Start is called before the first frame update
    void Start()
    {
        _consume = FindObjectOfType<Consume>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == _tagPlayer)
        {
            if(_consume.EatAmount > _requireSize)
            {
                Debug.Log("Win!");
            }
        }
    }
}
