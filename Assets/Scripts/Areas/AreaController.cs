using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaController : MonoBehaviour
{
    public string areaName = "None";
    public SpriteRenderer bgSprite;
    public TMPro.TMP_Text statText;

    public List<Cat> _cats = new();

    public abstract void UpdateTexts();
    public abstract void UpdateAreaState(Cat cat, bool addingCat);

    public abstract void NewDay();
    public virtual void AddCat(Cat cat)
    { 
        _cats.Add(cat);
        cat.inArea = true;
        cat.currArea = areaName;
        UpdateAreaState(cat, cat.inArea);
        cat.SetWanderBounds(bgSprite.bounds.min, bgSprite.bounds.max);
        GameManager.instance.UpdateAbilityIconsVisibility();
    }

    public virtual void RemoveCat(Cat cat)
    {
        _cats.Remove(cat);
        cat.inArea = false;
        cat.currArea = "None";
        UpdateAreaState(cat, cat.inArea);
        GameManager.instance.CatDefaultWander(cat);
        GameManager.instance.UpdateAbilityIconsVisibility();
    }

    public List<Cat> GetCats()
    {
        return _cats;
    }
    
    public virtual int GetNumCats()
    {
        return _cats.Count;
    }

    public void ClearCats()
    {
        _cats.Clear();
        UpdateTexts();
    }
    
    // Todo: Abstract Deal with New Day

}