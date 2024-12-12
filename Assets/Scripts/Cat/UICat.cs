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
    public TMP_Text abilityText;
    public TMP_Text abilityNameText;
    public TMP_Text nameText;
    public TMP_Text hoverStrengthText;
    public TMP_Text hoverHealthText;
    public TMP_Text hoverHuntingText;
    public GameObject hoverAbilityIcon;
    public Canvas uiCanvas;
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
        nameText.text = cat.CatName;
        hoverStrengthText.text = cat.Strength.ToString();
        hoverHealthText.text = cat.Health.ToString();
        hoverHuntingText.text = cat.Hunting.ToString();
        abilityNameText.text = cat.Ability.abilityName;
        abilityText.text = cat.Ability.description;
        hoverAbilityIcon.GetComponent<Image>().sprite = cat.Ability.icon;
        if (so.Pattern == null) pattern.color = Color.clear;
        if (so.Accessory == null) accessory.color = Color.clear;
    }

    public void DisableClick()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }
}
