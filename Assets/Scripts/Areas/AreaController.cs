using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaController : MonoBehaviour
{
    public string areaName = "None";
    public SpriteRenderer bgSprite;

    public List<Cat> _cats = new();

    public abstract void UpdateAreaState(Cat cat, bool addingCat);

    public abstract void NewDay();
    public virtual void AddCat(Cat cat)
    { 
        _cats.Add(cat);
        cat.inArea = true;
        cat.currArea = areaName;
        UpdateAreaState(cat, cat.inArea);
        cat.SetWanderBounds(bgSprite.bounds.min, bgSprite.bounds.max);
    }

    public virtual void RemoveCat(Cat cat)
    {
        _cats.Remove(cat);
        cat.inArea = false;
        cat.currArea = "None";
        UpdateAreaState(cat, cat.inArea);
        GameManager.instance.CatDefaultWander(cat);
    }

    public List<Cat> GetCats()
    {
        return _cats;
    }
    
    // Todo: Abstract Deal with New Day

}