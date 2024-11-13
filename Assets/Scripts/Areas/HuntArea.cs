using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntArea : AreaController
{
    [SerializeField] int _catCapacity;
    protected override int catCapacity => _catCapacity;
}