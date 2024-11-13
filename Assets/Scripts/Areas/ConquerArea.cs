using UnityEngine;

public class ConquerArea : AreaController
{
    [SerializeField] int _catCapacity;
    public Transform[] catSlots; // This can be assigned in the Unity Editor
    public override Transform[] CatSlots => catSlots;
    
    SpriteRenderer spriteRenderer;
    
    
    protected override int catCapacity => _catCapacity;
}