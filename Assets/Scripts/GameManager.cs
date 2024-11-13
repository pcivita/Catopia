using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    GameState gameState;
    [SerializeField] CatSO[] defaultCats;
    [SerializeField] GameObject catPrefab;

    List<Cat> catInstances;

    private void Start()
    {
        instance = this;
        catInstances = new List<Cat>();

        //create new game state
        gameState = GameState.NewGame();

        //construct all cats
        foreach(var c in gameState.GetCats()){
            ConstructCat(c);
        }
    }

    public CatSO[] GetDefaultCats()
    {
        return defaultCats;
    }

    void ConstructCat(CatSO c)
    {
        Cat newCat = Instantiate(catPrefab, Vector2.left, quaternion.identity).GetComponent<Cat>();
        newCat.Init(c);
        catInstances.Add(newCat);
    }
    

    /*
    void CreateDefaultCats()
    {
        Instantiate(catPrefab, Vector2.left, quaternion.identity).Init(defaultCatData[0]);
        Instantiate(catPrefab, Vector2.right, quaternion.identity).Init(defaultCatData[1]);
        Instantiate(catPrefab, Vector2.left * 2, quaternion.identity).Init(defaultCatData[0]);
        Instantiate(catPrefab, Vector2.right * 2, quaternion.identity).Init(defaultCatData[1]);
    }*/
}
