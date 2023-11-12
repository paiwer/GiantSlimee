using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPad : MonoBehaviour
{
    [SerializeField] private float _healAmount = 5;

    public float HealAmount => _healAmount;
}
