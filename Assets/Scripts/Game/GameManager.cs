using System;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameState gameState;
    [SerializeField] CatSO[] defaultCats;
    [SerializeField] GameObject catPrefab;
    [SerializeField] AreaController[] areas;
    [SerializeField] TMP_Text foodText;
    [SerializeField] TMP_Text consumptionText;
    [SerializeField] SpriteRenderer background;
    [SerializeField] GameObject LooseScreen;


    public List<Cat> catInstances;

    private void Awake()
    {
        instance = this;
        catInstances = new List<Cat>();
        if(gameState == null) gameState = GameState.NewGame();
        foreach (var c in gameState.GetCats()) ConstructCat(c);
        UpdateFoodText();
        UpdateConsumptionText();
    }

    public void UpdateFoodText()
    {
        foodText.text = "Food: " + gameState.GetFood();
    }
    
    public void UpdateConsumptionText()
    {
        consumptionText.text = "Consumption: " + (catInstances.Count);
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

    public void UpdateAllStats()
    {
        foreach (Cat catInstance in catInstances)
        {
            catInstance.UpdateStats();
        }
    }

    public void UpdateAbilityIconsVisibility()
    {
        foreach (Cat catInstance in catInstances)
        {
            Ability catAbility = catInstance.GetAbility();
            bool abilityIsActive = catAbility.IsActive(catInstance);
            if (abilityIsActive)
            {
                catInstance.abilityIcon.gameObject.SetActive(true);
            } else {
                catInstance.abilityIcon.gameObject.SetActive(false);
            }
        }
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
            gameState.NewDay();
            Debug.Log("AREAS Have Restarted, Total Food: " + gameState.GetFood());

            foreach (var area in areas)
            {
                area.NewDay();
            }

            foreach (var area in areas)
            {
                area.ClearCats();
            }

            Debug.Log("AREAS Have Restarted, Total Food: " + gameState.GetFood());
            foreach (var c in catInstances)
            {
                c.NewDay(true);
            }
            if (gameState.gameLost)
            {
                LoseGame("You didn't have enough food to feed your cats, game over!");
            }
            else
            {
                SceneManager.LoadScene("TestFight");
            }

        });
        
    }

    public static void LoseGame(string gameOverMessage)
    {
        Instantiate(instance.LooseScreen).GetComponent<LoseMenu>().SetGameoverMessage(gameOverMessage);
    }

    public static void ResetGame()
    {
        gameState = GameState.NewGame();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            LoseGame("Debug game over!");
        }
    }
}
