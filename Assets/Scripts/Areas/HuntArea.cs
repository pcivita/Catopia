using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HuntArea : AreaController
{
    public int totalHunting;
    [SerializeField] TrainArea trainArea;

    private void Start()
    {
        areaName = "Hunt";
        UpdateTexts();
    }

    public override void UpdateTexts()
    {
        statText.text = "Cat: " + _cats.Count +"\nHunt: " + GetTotalHuntingPlusBuffs();
    }

    public int GetTotalHuntingPlusBuffs()
    {
        int totalHuntingPlusBuffs = totalHunting;
        foreach (var cat in _cats) 
        {
            if (cat._catSO.Ability.abilityName == "Three Musketeers" && _cats.Count >= 3)
            {
                totalHuntingPlusBuffs *= 2;
            }
        }
        return totalHuntingPlusBuffs;
    }

    public override void UpdateAreaState(Cat cat, bool addingCat)
    {
        if (addingCat)
        {
            totalHunting += cat.GetHuntingPlusBuffs();  
        }
        else
        {
            totalHunting -= cat.GetHuntingPlusBuffs();
        }

        Debug.Log("RUNNING AFTER RESET");
        trainArea.UpdateTexts();
        UpdateTexts();
    }

    public override void NewDay()
    {
        GameManager.gameState.AddFood(GetTotalHuntingPlusBuffs());
        totalHunting = 0;

        UpdateTexts();
    }
}