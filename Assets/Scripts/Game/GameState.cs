using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All data that needs to be stored about our game. Do not use for visualization purposes.
public class GameState {

    protected List<CatSO> colonyMembers;
    protected int foodCount;

    //NOT FINAL!
    //TODO how do we want to seed the colony? could randomize
    static List<CatSO> InitalizeCats()
    {
        Debug.Log("Initialize cats");
        List<CatSO> l = new List<CatSO>();
        l.Add(GameManager.instance.GetDefaultCats()[0]);
        l.Add(GameManager.instance.GetDefaultCats()[1]);
        return l;
    }

    public static GameState NewGame()
    {
        Debug.Log("Call new game");
        GameState state = new GameState();
        state.colonyMembers = InitalizeCats();
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

    public void AddCat(CatSO c)
    {
        Debug.Log("Add cat " + c.CatName);
        colonyMembers.Remove(c);
    }

    public int GetFood()
    {
        return foodCount;
    }

    public bool TryConsumeFood(int amount)
    {
        if (amount > foodCount)
        {
            return false;
        }
        else
        {
            foodCount -= amount;
            return true;
        }
    }

    public void AddFood(int amount)
    {
        foodCount += amount;
    }


}
