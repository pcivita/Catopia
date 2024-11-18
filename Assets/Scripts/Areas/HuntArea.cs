using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HuntArea : AreaController
{
    public int totalHunting;
    private TMP_Text catTextMesh;

    [SerializeField] TrainArea trainArea;
    private TMP_Text huntTextMesh;

    private void Start()
    {
        areaName = "Hunt";
        TMP_Text[] textMeshes = gameObject.GetComponentsInChildren<TMP_Text>();
        
        if (textMeshes.Length >= 2)
        {
            catTextMesh = textMeshes[0];
            huntTextMesh = textMeshes[1];
        }

        UpdateTexts();
    }

    public void UpdateTexts()
    {
        catTextMesh.text = "Cat: " + _cats.Count;
        huntTextMesh.text = "Hunt: " + GetTotalHuntingPlusBuffs();
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
            totalHunting += cat._catSO.Hunting;  
        }
        else
        {
            totalHunting -= cat._catSO.Hunting;
        }
        trainArea.SetCapacity();
        trainArea.UpdateTexts();
        UpdateTexts();
    }

    public override void NewDay()
    {
        GameManager.instance.gameState.AddFood(GetTotalHuntingPlusBuffs());
        totalHunting = 0;
        _cats.Clear();

        Debug.Log("Hunt Cleared");

        UpdateTexts();
    }
}