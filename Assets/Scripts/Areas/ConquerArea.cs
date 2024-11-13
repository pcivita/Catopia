using UnityEngine;

public class ConquerArea : AreaController
{
    
    SpriteRenderer spriteRenderer;
    
    [SerializeField] int _catCapacity;
    protected override int catCapacity => _catCapacity;
}