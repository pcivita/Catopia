using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitButton : MonoBehaviour
{
    // Start is called before the first frame update
    public CatSO catSo;
    public TMPro.TMP_Text costText;
    void Start()
    {
        if (catSo != null)
        {
        costText.text = catSo.Cost.ToString();
        }
        else
        { 
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
