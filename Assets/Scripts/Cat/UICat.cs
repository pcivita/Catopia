using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class UICat : MonoBehaviour
{
    public TMP_Text foodCost;
    public TMP_Text infoText;

    Button button;
    public Image body;
    public Image pattern;
    public Image accessory;
    CatSO cat;
    // Start is called before the first frame update

    public void SetOnClick(Action onclick)
    {
        button = GetComponent<Button>();
        button.interactable = true;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onclick());
    }

    public void SetCatSO(CatSO so)
    {
        if(so == null)
        {
            Debug.Log("CatUI set cat is null, deactivating");
            gameObject.SetActive(false);
            return;
        }
        cat = so;
        body.color = so.bodyColor;
        pattern.color = so.patternColor;
        pattern.sprite = so.Pattern;
        accessory.sprite = so.Accessory;
        foodCost.text = "cost: "+so.Cost+" food";
        infoText.text = cat.LogInfo();
    }

    public void DisableClick()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }
}
