using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaController : MonoBehaviour
{
    //Later on make this an SO
    protected abstract int catCapacity {get;}
    
    public abstract Transform[] CatSlots { get; }
    protected List<Cat> cats = new();

    private void OnCatReceived(Cat cat)
    {
        int index = GetFirstAvailableSlot();
        if (index == -1)
        {
            Debug.LogWarning("No available slots for the new cat.");
            return;
        }

        // Ensure the list has enough space for the new cat
        if (index >= cats.Count)
        {
            cats.Add(cat);
        }
        else
        {
            cats[index] = cat;
        }

        cat.inArea = true;
        Debug.Log(index);
        cat.transform.position = CatSlots[index].position;
    }

    
    private int GetFirstAvailableSlot()
    {
        for (int i = 0; i < CatSlots.Length; i++)
        {
            if (i >= cats.Count || cats[i] == null)
            {
                return i;
            }
        }
        return -1; // No available slots
    }

    public void RemoveCat(Cat cat)
    {
        int index = cats.IndexOf(cat);
        if (index != -1)
        {
            cats[index] = null; // Clear the slot
            cat.inArea = false;
            Debug.Log($"Cat removed from slot {index}");
        }
    }


    public bool TryReceiveCat(Cat cat)
    {
        int availableSlot = GetFirstAvailableSlot();
        if (availableSlot == -1) return false;  // No available slots

        OnCatReceived(cat);
        return true;
    }
    

}