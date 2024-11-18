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
    [SerializeField] TMP_Text consumptionText;
    [SerializeField] SpriteRenderer background;


    public List<Cat> catInstances;

    private void Awake()
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
        UpdateFoodText();
        UpdateConsumptionText();
    }

    public void UpdateFoodText()
    {
        foodText.text = "Food: " + gameState.GetFood();
    }
    
    public void UpdateConsumptionText()
    {
        consumptionText.text = "Consumption: " + (catInstances.Count + areas[1]._cats.Count);
    }

    public CatSO[] GetDefaultCats()
    {
        return defaultCats;
    }

    public void CatDefaultWander(Cat cat)
    {
        cat.SetWanderBounds(background.bounds.min, background.bounds.max);
    }


    public void ConstructCat(CatSO c)
    {
        //TODO randomize position of spawn.
        Vector2 pos = Vector2.left * UnityEngine.Random.Range(-2f, 2f) + Vector2.right * UnityEngine.Random.Range(-2f, 2f);
        Cat newCat = Instantiate(catPrefab, pos, quaternion.identity).GetComponent<Cat>();
        newCat.Init(c);
        catInstances.Add(newCat);
        CatDefaultWander(newCat);
    }

    public static CatSO GetRandomDefaultCat()
    {
        return instance.defaultCats[UnityEngine.Random.RandomRange(0, instance.defaultCats.Length)].Clone();
    }

    //TODO stub
    //do whatever happens at the end of a turn
    //notify the areas
    //consume food....
    //kill cats accordingly?
    public void NewDay()
    {
        //triggers event
        GameObject.FindFirstObjectByType<EventManager>().PlayEvent(() =>
        {
            //all other logic happens once event is over. When event window is closed, this code executes.
            gameState.NewDay();

            foreach (var c in catInstances)
            {
                c.NewDay();
            }
            foreach (var area in areas)
            {
                Debug.Log(area.areaName);
                area.NewDay();
            }

        });
        

    }
}
