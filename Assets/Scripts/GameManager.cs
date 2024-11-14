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
        foreach (var c in gameState.GetCats())
        {
            ConstructCat(c);
        }
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            NewDay();
        }
    }

    public CatSO[] GetDefaultCats()
    {
        return defaultCats;
    }

    void ConstructCat(CatSO c)
    {
        //TODO randomize position of spawn.
        Vector2 pos = Vector2.left * UnityEngine.Random.Range(-2f, 2f) + Vector2.right * UnityEngine.Random.Range(-2f, 2f);
        Cat newCat = Instantiate(catPrefab, pos, quaternion.identity).GetComponent<Cat>();
        newCat.Init(c);
        catInstances.Add(newCat);
    }

    //TODO stub
    //do whatever happens at the end of a turn
    //notify the areas
    //consume food....
    //kill cats accordingly?
    void NewDay()
    {
        Debug.Log("New day.");
        foreach (var c in catInstances)
        {
            c.ResetPosition();
        }
    }
}
