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

    public GameObject warning;

    private int hunting;
    private int catCapacity;
    private void Start()
    {
        areaName = "Train";
        currentTraining = trainingList[(GameManager.gameState.GetDay() - 1) % trainingList.Length];
        Debug.Log(currentTraining);
        
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
        SetCapacity();
    }
    
    public void SetCapacity() {
        food = GameManager.gameState.GetFood();
        numCats = GameManager.gameState.GetCats().Count;
        hunting = huntArea.totalHunting;
        catCapacity = food + hunting - numCats;
        UpdateTexts();
    }
    public override void UpdateTexts()
    {
        statText.text = "+4 " + currentTraining;

        if (_cats.Count > catCapacity) {
            warning.SetActive(true);
        } else {
            warning.SetActive(false);
            statText.text += "\nCat: " + _cats.Count + " / " + catCapacity;
        }
    }

    public override void UpdateAreaState(Cat cat, bool addingCat)
    {
        UpdateTexts();
        // PlayerLog.instance.AddEvent("TESTING");
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
        SetCapacity();
        GameManager.instance.UpdateConsumptionText();
    }

    public string getCurrentTraining()
    {
        return currentTraining;
    }

}