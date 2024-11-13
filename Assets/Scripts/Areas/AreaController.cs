using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaController : MonoBehaviour
{
    //Later on make this an SO
    protected abstract int catCapacity {get;}
    protected List<Cat> cats = new();

    private void OnCatReceived(Cat cat)
    {
        cats.Add(cat);
        // Will the cat animate itself?
        
    }
    
    

    public bool TryReceiveCat(Cat cat)
    {
        if (cats.Count >= catCapacity) return false;
        OnCatReceived(cat);




        return true;
    }
    

}