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

    public GameObject warning;


    public List<Cat> catInstances;

    private void Awake()
    {
        instance = this;
        catInstances = new List<Cat>();
        if(gameState == null) gameState = GameState.NewGame();
        foreach (var c in gameState.GetCats()) ConstructCat(c);
        UpdateFoodText();
        UpdateConsumptionText();
        CheckWarning();
        Debug.Log("CHECKED WARNING");
    }


    private int previousFoodValue = 0; // Keep track of the last food value

    public void UpdateFoodText()
    {
         HuntArea huntArea = (HuntArea)areas[0];
        // Calculate the difference
        int newFoodValue = gameState.GetFood() + huntArea.GetTotalHuntingPlusBuffs();
        int difference = newFoodValue - previousFoodValue;

        // Update the text
        foodText.text = newFoodValue.ToString();

        // Determine the color and start the coroutine
        if (difference > 0)
        {
            StartCoroutine(FlashTextColor(Color.green, 2f));
        }
        else if (difference < 0)
        {
            StartCoroutine(FlashTextColor(Color.red, 2f));
        }

        // Update the previous value
        previousFoodValue = newFoodValue;
    }

    private IEnumerator FlashTextColor(Color color, float duration)
    {
        Color originalColor = foodText.color; // Store the original color
        foodText.color = color; // Change to the temporary color

        yield return new WaitForSeconds(duration); // Wait for the specified time

        foodText.color = Color.white; // Revert back to the original color
    }
    public void UpdateConsumptionText()
    {
        consumptionText.text = catInstances.Count.ToString();
    }


    public void CheckWarning() {
        if (gameState.GetFood() < catInstances.Count) {
            warning.gameObject.SetActive(true);
        } else {
            warning.gameObject.SetActive(false);
        }
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
        Vector2 pos = Vector2.left * UnityEngine.Random.Range(1f, 5f);
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
        // if (Input.GetKeyUp(KeyCode.Space))
        // {
        //     LoseGame("Debug game over!");
        // }
    }
}
