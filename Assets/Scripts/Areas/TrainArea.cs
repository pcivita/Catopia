using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrainArea : AreaController
{
    
    public string[] trainingList = { "hunt", "strength", "health" };
    public string currentTraining;
    private TMP_Text catTextMesh;
    private TMP_Text trainTextMesh;
    private void Start()
    {
        currentTraining = trainingList[GameManager.instance.gameState.GetDay() % 3 - 1];
        Debug.Log(currentTraining);

        TMP_Text[] textMeshes = gameObject.GetComponentsInChildren<TMP_Text>();

            if (textMeshes.Length >= 2)
            {
                catTextMesh = textMeshes[0];
                trainTextMesh = textMeshes[1];
            }
        
    }
    
    public void UpdateTexts()
    {
        catTextMesh.text = "Cat: " + _cats.Count;
        trainTextMesh.text = "Training: " + currentTraining;
    }

    public override void UpdateAreaState(Cat cat, bool addingCat)
    {
        // if (addingCat)
        // {
        //     totalHunting += cat._catSO.Hunting;  
        // }
        // else
        // {
        //     totalHunting -= cat._catSO.Hunting;
        // }

        UpdateTexts();
    }

    public override void NewDay()
    {
        
        throw new System.NotImplementedException();
    }
}
