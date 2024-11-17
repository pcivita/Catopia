using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState gameState;
    [SerializeField] CatSO[] defaultCats;
    [SerializeField] GameObject catPrefab;
    [SerializeField] AreaController[] areas;
    [SerializeField] TMP_Text foodText;

    List<Cat> catInstances;

    private void Awake()
    {
        instance = this;
        catInstances = new List<Cat>();
        
        //create new game state
        gameState = GameState.NewGame();
        UpdateFoodText();

        //construct all cats
        foreach (var c in gameState.GetCats())
        {
            ConstructCat(c);
        }
    }

    public void UpdateFoodText()
    {
        foodText.text = "Food: " + gameState.GetFood();
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
    public void NewDay()
    {
        // First we Create a new Day
        gameState.NewDay();
       
        // Then, we reset the position of each Cat
        foreach (var c in catInstances)
        {
            c.NewDay();
        }
        // Finally We Deal with the areas
        foreach (var area in areas)
        {
            area.NewDay();
        }

      
    }
}
