using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrainArea : AreaController
{
    public string[] trainingList = { "Hunting", "Strength", "Health" };
    public string currentTraining;

    public HuntArea huntArea;
    private int food;

    private int numCats;


    private int hunting;
    private void Start()
    {
        areaName = "Train";
        currentTraining = trainingList[(GameManager.gameState.GetDay() - 1) % trainingList.Length];
        
        // Initialize huntArea before calling SetCapacity
        if (huntArea == null)
        {
            huntArea = FindObjectOfType<HuntArea>(); // Or assign it appropriately
            if (huntArea == null)
            {
                Debug.LogError("HuntArea is not found!");
                return;
            }
        }
        UpdateTexts();
    }
    
    public override void UpdateTexts()
    {
        statText.text = "+4 " + currentTraining;
    }

    public override void UpdateAreaState(Cat cat, bool addingCat)
    {
        UpdateTexts();
    }

    public override void NewDay()
    {
      
        foreach (var cat in _cats)
        {
            Debug.Log(cat._catSO.CatName);
            cat.Train();
        }

        currentTraining = trainingList[(GameManager.gameState.GetDay() - 1) % trainingList.Length];
    }

    public string getCurrentTraining()
    {
        return currentTraining;
    }

}