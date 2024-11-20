using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSlot : MonoBehaviour
{
    // Start is called before the first frame update
    
    // Opposing Slot

    public bool isOccupied;
    
    // is Friend
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool TryAddCat(FightCat cat) // This tries first
    {
        if (isOccupied)
        {
            return false;
        }
        cat.transform.position = this.transform.position;
        isOccupied = true;
        return true;
    }
}
