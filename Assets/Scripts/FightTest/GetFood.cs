using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetFood : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TMP_Text foodText;
    void Start()
    {
        foodText.text = GameManager.gameState.GetFood().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
