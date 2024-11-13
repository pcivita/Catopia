using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainArea : AreaController
{
    [SerializeField] int _catCapacity;
    private Transform[] catSlots; // This can be assigned in the Unity Editor

    public override Transform[] CatSlots => catSlots;
    
    SpriteRenderer spriteRenderer;
    
    
    protected override int catCapacity => _catCapacity;
}
