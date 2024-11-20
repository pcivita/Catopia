using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    
    public static BattleManager instance;
    
    [SerializeField]
    private Transform[] enemySlots;
    public FightSlot[] fightSlots;
    
    [SerializeField] CatSO[] EnemySOs;
    [SerializeField] GameObject fightCatPrefab;
    
    // TODO: Get from Data
    private FightCat[] enemyCats;
    
    
    [SerializeField] private Button myButton; // Reference to the button
    private bool conditionMet = false; // Your condition to enable the button

    
    void Awake()
    {
        instance = this;
        for (int i = 0; i < EnemySOs.Length; i++)
        {
            Vector2 pos = enemySlots[i].transform.position;
            FightCat newCat = Instantiate(fightCatPrefab, pos, quaternion.identity).GetComponent<FightCat>();
            newCat.InitEnemy(EnemySOs[i]);
            // catInstances.Add(newCat);
        }
        myButton.interactable = false;
    }
    
    void Update()
    {
        
       
    }
    

    public void CheckButtonCondition()
    {
        if (AllEnemiesHavePairs())
        {
            myButton.interactable = true;
        }
        else
        {
            myButton.interactable = false;
        }
    }

    private bool AllEnemiesHavePairs()
    {
        for (int i = 0; i < EnemySOs.Length; i++)
        {
            if (!fightSlots[i].isOccupied)
            {
            return false;
            }
        }

        return true;
    }

    public void StartBattle()
    {
        Debug.Log("StartBattle");
    }
    
    
   
}
