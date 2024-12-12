using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ConquerArea : AreaController
{
    [SerializeField]
    private Transform[] catSlots; // Backing field

    private int catCapacity = 3;

    private void Start()
    {
        areaName = "Conquer";
        UpdateTexts();
    }

    public override void UpdateTexts()
    {
        UpdateConquerCapacity();
        int nonNullCatCount = _cats.Count(cat => cat != null);
        statText.text = "Cat: " + nonNullCatCount + "/" + catCapacity;
    }

    public void UpdateNewDayVisibility()
    {
        // GameObject newDayButton = GameObject.Find("MainCanvas").transform.Find("newDay").gameObject;
        GameObject newDayButtonObject = GameObject.Find("Befriend");
        Button newDayButton = newDayButtonObject.GetComponent<Button>();
        if (newDayButton == null)
        {
            return;
        }

        TMP_Text buttonText = newDayButtonObject.GetComponentInChildren<TMP_Text>();
        if (buttonText == null) 
        {
            return;
        }

        int nonNullCatCount = _cats.Count(cat => cat != null);
        if (nonNullCatCount == catCapacity && catCapacity > 0)
        {
            newDayButton.interactable = true;
            buttonText.color = new Color(69f / 255f, 16f / 255f, 25f / 255f, 1f);
        } else {
            newDayButton.interactable = false;
            buttonText.color = new Color(69f / 255f, 16f / 255f, 25f / 255f, 0.4f);
        }
    }

    public void UpdateConquerCapacity()
    {
        int levelNumber = GameManager.gameState.GetDay();
        string path = $"Level {levelNumber}";
        CatSO[] enemySOs = Resources.LoadAll<CatSO>(path);
        catCapacity = enemySOs.Length;
    }

    public override void AddCat(Cat cat) // This tries first
    {
        int availableSlot = GetFirstAvailableSlot();
        if (availableSlot == -1)
        {
            Debug.Log("No available slot");
        }
        else
        {
            // Ensure the list has enough space for the new cat
            if (availableSlot >= _cats.Count)
            {
                _cats.Add(cat);
            }
            else
            {
                _cats[availableSlot] = cat;
            }
            cat.inArea = true;
            cat.inConquer = true;
            cat.currArea = areaName;
            Debug.Log(availableSlot);
            cat.transform.position = catSlots[availableSlot].position;
        }
        UpdateNewDayVisibility();
        UpdateTexts();
        GameObject huntObject = GameObject.Find("Hunt");
        if (huntObject == null) { return; }
        HuntArea huntArea = huntObject.GetComponent<HuntArea>();
        if (huntArea == null) { return; }
        huntArea.UpdateTotalHunting();
        GameManager.instance.UpdateAbilityIconsVisibility();
        GameManager.instance.UpdateAllStats();
        
    }

    private int GetFirstAvailableSlot()
    {
        for (int i = 0; i < catSlots.Length; i++)
        {
            if ((i >= _cats.Count || _cats[i] == null) && i < catCapacity)
            {
                return i;
            }
        }
        return -1; // No available slots
    }
    
    public override int GetNumCats()
    {
        int count = 0;
        for (int i = 0; i < _cats.Count; i++)
        {
            if (_cats[i] != null)
            {
                count++;
            }
        }
        Debug.Log("COUNT" + count);
        return count; 
    }
    
    public override void RemoveCat(Cat cat)
    {
        int index = _cats.IndexOf(cat);
        if (index != -1)
        {
            _cats[index] = null; // Clear the slot
            cat.inArea = false;
            cat.inConquer = false;
            cat.currArea = "None";
            Debug.Log($"Cat removed from slot {index}");
        }
        GameObject huntObject = GameObject.Find("Hunt");
        if (huntObject == null) { return; }
        HuntArea huntArea = huntObject.GetComponent<HuntArea>();
        if (huntArea == null) { return; }
        huntArea.UpdateTotalHunting();
        UpdateNewDayVisibility();
        UpdateTexts();
        GameManager.instance.UpdateAbilityIconsVisibility();
        GameManager.instance.UpdateAllStats();
    }
    
    public override void UpdateAreaState(Cat cat, bool addingCat)
    {

        Debug.Log("RUIN THIS IS THE CAT");
        UpdateTexts();
    }

    public override void NewDay()
    {
        BattleManager.team.Clear();
        foreach(Cat cat in _cats)
        {
            if (cat == null) continue;
            CatSO clone = cat._catSO.Clone();
            clone.Strength = cat.GetStrengthPlusBuffs();
            clone.Health = cat.GetHealthPlusBuffs();
            BattleManager.team.Add(clone);
            BattleManager.levelNumber = GameManager.gameState.GetDay();
        }
        UpdateNewDayVisibility();
        UpdateTexts();
    }
}