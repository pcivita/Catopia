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
    private void Start()
    {
        currentTraining = trainingList[(GameManager.instance.gameState.GetDay() - 1) % trainingList.Length];
        Debug.Log(currentTraining);

        TMP_Text[] textMeshes = gameObject.GetComponentsInChildren<TMP_Text>();

        if (textMeshes.Length >= 2)
        {
            catTextMesh = textMeshes[0];
            trainTextMesh = textMeshes[1];
        }

        UpdateTexts();
    }
    
    public void UpdateTexts()
    {
        catTextMesh.text = "Cat: " + _cats.Count;
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
        
        currentTraining = trainingList[(GameManager.instance.gameState.GetDay() - 1) % trainingList.Length];
        UpdateTexts();
    }
}