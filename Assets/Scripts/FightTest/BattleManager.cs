using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] enemySlots;
    
    [SerializeField] CatSO[] EnemySOs;
    [SerializeField] GameObject fightCatPrefab;
    
    // TODO: Get from Data
    private FightCat[] enemyCats;
    // Start is called before the first frame update
    
    void Awake()
    {
        for (int i = 0; i < EnemySOs.Length; i++)
        {
            Vector2 pos = enemySlots[i].transform.position;
            FightCat newCat = Instantiate(fightCatPrefab, pos, quaternion.identity).GetComponent<FightCat>();
            newCat.InitEnemy(EnemySOs[i]);
            // catInstances.Add(newCat);
        }
    }
    
    void Update()
    {
        // Check if every EnemyCat's Slot has its conterpart slot occupied: 
        
    }

    public void StartBattle()
    {
        Debug.Log("StartBattle");
    }
    
    
   
}
