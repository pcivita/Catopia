using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;


public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    
    
    public static List<CatSO> team = new List<CatSO>();
    public static int levelNumber;
    
    [SerializeField]
    private Transform[] enemySlots;
    public FightSlot[] fightSlots;
    
    public CatSO[] EnemySOs;
    [SerializeField] GameObject fightCatPrefab;
    
    // TODO: Get from Data
    private List<FightCat> enemyCats = new List<FightCat>();
    
    public List<GameObject> LoseSlots = new List<GameObject>();
    public List<GameObject> WinSlots = new List<GameObject>();
    public List<GameObject> TieSlots = new List<GameObject>();
    
    
    public GameObject WinPanel;
    public GameObject TiePanel;
    public GameObject LosePanel;


    
    public bool inBattle;
    
    
    [SerializeField] private Button myButton; // Reference to the button
    
    [SerializeField] private List<RecruitButton> recruitButtons = new List<RecruitButton>();
    private bool conditionMet = false; // Your condition to enable the button

    private int score;

    
    void Awake()
    {
        instance = this;
        //TODO: Make this every three days
        string path = $"Level {levelNumber - 1}";
        Debug.Log(path);
        EnemySOs = Resources.LoadAll<CatSO>(path);
        
        Debug.Log(EnemySOs.Length);
        for (int i = 0; i < EnemySOs.Length; i++)
        {
            Vector2 pos = enemySlots[i].transform.position;
            FightCat newCat = Instantiate(fightCatPrefab, pos, quaternion.identity).GetComponent<FightCat>();
            newCat.Init(EnemySOs[i],false);
            enemyCats.Add(newCat);
            recruitButtons[i].catSo = (EnemySOs[i]);
            if (recruitButtons[i].catSo.Cost > GameManager.gameState.GetFood())
            {
                Button recruitButton = recruitButtons[i].GetComponentInParent<Button>();
                recruitButton.interactable = false;
            }
        }
        myButton.interactable = false;

        foreach(CatSO cat in team)
        {
            Vector2 pos = new Vector2(-5f, UnityEngine.Random.RandomRange(-3f,3f));
            FightCat newCat = Instantiate(fightCatPrefab, pos, quaternion.identity).GetComponent<FightCat>();
            newCat.Init(cat, true);
        }
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
        Debug.Log("All Enemies have pairs");
        for (int i = 0; i < EnemySOs.Length; i++)
        {
            Debug.Log("BEFORE" + i);
            if (!fightSlots[i].isOccupied)
            { 
                Debug.Log(i);
            return false;
            }
        }

        return true;
    }

    public void StartBattle()
    {
        inBattle = true;
        myButton.interactable = false;
        StartCoroutine(HandleBattles());
    }

    private IEnumerator HandleBattles()
    {
        for (int i = 0; i < enemyCats.Count; i++)
        {
            FightCat yourCat = fightSlots[i].fightCat;
            FightCat enemyCat = enemyCats[i];
            // Wait for this fight to finish
            yield return StartCoroutine(Fight(yourCat, enemyCat, i));
        }
        yield return new WaitForSeconds(1);
        inBattle = false; // Battle sequence finished
        if (score > 0)
        {
            // resultUI.Open("")
            WinPanel.SetActive(true);
        } else if (score < 0)
        {
            LosePanel.SetActive(true);
            
        }
        else
        {
            TiePanel.SetActive(true);
        }
    }

    private IEnumerator Fight(FightCat yourCat, FightCat enemyCat, int index)
    {
        // Fight to the Death
        while (yourCat.health > 0 && enemyCat.health > 0)
        {
            // Shake both cats
            Coroutine yourCatShake = StartCoroutine(yourCat.Shake(yourCat.transform, 1.0f, 0.1f, Random.Range(0f, 1f)));
            Coroutine enemyCatShake = StartCoroutine(enemyCat.Shake(enemyCat.transform, 1.0f, 0.1f, Random.Range(1f, 2f)));
            
            yield return yourCatShake;
            yield return enemyCatShake;

            // Subtract health
            yourCat.health -= enemyCat.attack;
            enemyCat.health -= yourCat.attack;

            // Update UI texts
            yourCat.UpdateTexts();
            enemyCat.UpdateTexts();

            // Wait before the next round
            yield return new WaitForSeconds(0.5f);
        }

        // Determine outcome
        if (yourCat.health <= 0 && enemyCat.health <= 0)
        {
            // Todo: Set Name
            TieSlots[index].SetActive(true);
        }
        else if (yourCat.health > 0)
        {
            WinSlots[index].SetActive(true);
            TMP_Text displayText = WinSlots[index].GetComponentInChildren<TMP_Text>();
            displayText.text = yourCat._catSO.CatName + " Wins!";
            score++;
        }
        else
        {
            TMP_Text displayText = LoseSlots[index].GetComponentInChildren<TMP_Text>();
            displayText.text = enemyCat._catSO.CatName + " Wins...";
            LoseSlots[index].SetActive(true);
            score--;
        }
    }

    public void NewGame()
    {
        GameManager.gameState = GameState.NewGame();
    }

    public void ReturnToColony()
    {
        SceneManager.LoadScene("ColonyScene");
    }
   
}
