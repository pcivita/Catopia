using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrainArea : AreaController
{
    public string[] trainingList = { "Hunting", "Strength", "Health" };
    public string currentTraining;
    private TMP_Text catTextMesh;
    private TMP_Text trainTextMesh;

    public HuntArea huntArea;
    private int food;

    private int numCats;

    private int hunting;
    private int catCapacity;
    private void Start()
    {
        areaName = "Train";
        currentTraining = trainingList[(GameManager.instance.gameState.GetDay() - 1) % trainingList.Length];
        Debug.Log(currentTraining);

        TMP_Text[] textMeshes = gameObject.GetComponentsInChildren<TMP_Text>();

        if (textMeshes.Length >= 2)
        {
            catTextMesh = textMeshes[0];
            trainTextMesh = textMeshes[1];
        }

        SetCapacity();
    }
    
    public void SetCapacity() {
        food = GameManager.instance.gameState.GetFood();
        numCats = GameManager.instance.gameState.GetCats().Count;
        hunting = huntArea.totalHunting;
        catCapacity = food + hunting - numCats;
        UpdateTexts();
    }
    public void UpdateTexts()
    {
        if (_cats.Count > catCapacity) {
            catTextMesh.text = "Warning you will Literally Die!";
        } else {
            catTextMesh.text = "Cat: " + _cats.Count + " / " + catCapacity;
        }
        trainTextMesh.text = currentTraining;
    }

    public override void UpdateAreaState(Cat cat, bool addingCat)
    {
        UpdateTexts();
        GameManager.instance.UpdateConsumptionText();
    }

    public override void NewDay()
    {
      
        foreach (var cat in _cats)
        {
            Debug.Log(cat._catSO.CatName);
            cat.Train(currentTraining, 1);
        }
        
        _cats.Clear();
        
        Debug.Log("Train Cleared");

        currentTraining = trainingList[(GameManager.instance.gameState.GetDay() - 1) % trainingList.Length];
        UpdateTexts();
    }

}