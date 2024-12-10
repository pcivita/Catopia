using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FurnitureSO", menuName = "ScriptableObjects/Furniture")]
public class Furniture : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public GameObject Prefab;
    public int Cost;

    public void Construct()
    {
        Instantiate(Prefab);
    }
}
