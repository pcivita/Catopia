using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntArea : AreaController
{
    public int totalHunting;


    public override void UpdateAreaState(Cat cat, bool addingCat)
    {
        if (addingCat)
        {
            totalHunting += cat._catSO.Hunting;  
        }
        else
        {
            totalHunting -= cat._catSO.Hunting;
        }
    }

    public override void NewDay()
    {
        GameManager.instance.gameState.AddFood(totalHunting);
        totalHunting = 0;
        _cats.Clear();
    }
}