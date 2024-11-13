using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public static Player Instance;
    void Awake()
    {
        if (Instance == null) Instance = this; 
        else Destroy(gameObject);
    }
    
    void OnDestroy()
    {
        if (this == Instance) Instance = null;
    }
}
