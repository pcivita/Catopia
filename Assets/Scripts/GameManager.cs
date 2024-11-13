using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CatSO defaultCat1Data;
    [SerializeField] CatSO defaultCat2Data;
    // If there are problems: Change to GameObject
    [SerializeField] Cat catPrefab;


    private void Start()
    {
        CreateDefaultCats();
    }

    void CreateDefaultCats()
    {
        
        Instantiate(catPrefab, Vector2.left, quaternion.identity).Init(defaultCat1Data);
        Instantiate(catPrefab, Vector2.right, quaternion.identity).Init(defaultCat2Data);
    }
}
