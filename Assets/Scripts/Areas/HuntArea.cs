using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HuntArea : AreaController
{
    public int totalHunting;
    private TMP_Text catTextMesh;
    private TMP_Text huntTextMesh;

    private void Start()
    {
        TMP_Text[] textMeshes = gameObject.GetComponentsInChildren<TMP_Text>();
        Debug.Log(textMeshes.Length);
        
        if (textMeshes.Length >= 2)
        {
            catTextMesh = textMeshes[0];
            huntTextMesh = textMeshes[1];
        }
        catTextMesh.text = "Cat: " + _cats.Count;
        huntTextMesh.text = "Hunt: " + totalHunting;
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
        catTextMesh.text = "Cat: " + _cats.Count;
        huntTextMesh.text = "Hunt: " + totalHunting;
    }

    public override void NewDay()
    {
        GameManager.instance.gameState.AddFood(totalHunting);
        totalHunting = 0;
        _cats.Clear();
        Debug.Log( GameManager.instance.gameState.GetFood());
        catTextMesh.text = "Cat: " + _cats.Count;
        huntTextMesh.text = "Hunt: " + totalHunting;
    }
}