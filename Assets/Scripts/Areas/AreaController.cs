using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaController : MonoBehaviour
{
    protected List<Cat> _cats = new();

    public abstract void UpdateAreaState(Cat cat, bool addingCat);

    public abstract void NewDay();
    public void AddCat(Cat cat)
    { 
        _cats.Add(cat);
        cat.inArea = true;
        UpdateAreaState(cat, cat.inArea);
    }
    
    public void RemoveCat(Cat cat)
    {
        _cats.Remove(cat);
        cat.inArea = false;
        UpdateAreaState(cat, cat.inArea);
    }
    
    // Todo: Abstract Deal with New Day

}