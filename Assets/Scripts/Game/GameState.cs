using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All data that needs to be stored about our game. Do not use for visualization purposes.
public class GameState {

    protected List<CatSO> colonyMembers;
    protected int foodCount;
    protected int dayCount = 1;
    public int mapNode = 0;
    public bool gameLost = false;

    //NOT FINAL!
    //TODO how do we want to seed the colony? could randomize
    static List<CatSO> InitalizeCats(int number)
    {
        Debug.Log("Initialize cats");
        List<CatSO> l = new List<CatSO>();
        CatSO[] defaultCats = GameManager.instance.GetDefaultCats();
        for(int i = 0; i < number; i++)
        {
            l.Add(defaultCats[UnityEngine.Random.RandomRange(0, defaultCats.Length)].Clone());
        }
        return l;
    }

    public static GameState NewGame()
    {
        Debug.Log("Call new game");
        GameState state = new GameState();
        state.colonyMembers = InitalizeCats(4);
        state.foodCount = 5;
        return state;
    }

    public List<CatSO> GetCats()
    {
        return colonyMembers;
    }

    public void KillCat(CatSO c)
    {
        Debug.Log("Killed cat " + c.CatName);
        colonyMembers.Remove(c);
    }

    public void AddCat(CatSO c, bool constructNow = true)
    {
        Debug.Log("Add cat " + c.CatName);
        colonyMembers.Add(c);
        if(constructNow)GameManager.instance.ConstructCat(c);
    }

    public int GetFood()
    {
        return foodCount;
    }

    public int GetDay()
    {
        return dayCount;
    }
    public bool TryConsumeFood(int amount)
    {
        if (amount > foodCount)
        {
            gameLost = true;
            Debug.Log("You HAVE LITERALLY LOST THE GAME");
            return false;
        }
        else
        {
            foodCount -= amount;
            GameManager.instance.UpdateFoodText();
            return true;
        }
    }

    public void AddFood(int amount)
    {
        foodCount += amount;
        GameManager.instance.UpdateFoodText();
    }

    public void NewDay()
    {
        dayCount++;
    }
}
