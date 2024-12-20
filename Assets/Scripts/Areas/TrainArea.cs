using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrainArea : AreaController
{
    public string[] trainingList = new string[5];
    public string currentTraining;

    public HuntArea huntArea;
    private int food;

    private int numCats;


    private int hunting;
    private void Start()
    {
        areaName = "Train";
        trainingList[0] = "Health";
        trainingList[1] = "Strength";
        trainingList[2] = "Hunting";
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
        GameObject huntObject = GameObject.Find("Hunt");
        if (huntObject == null) { return; }
        HuntArea huntArea = huntObject.GetComponent<HuntArea>();
        if (huntArea == null) { return; }
        huntArea.UpdateTotalHunting();
        GameManager.instance.UpdateConsumptionText();
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