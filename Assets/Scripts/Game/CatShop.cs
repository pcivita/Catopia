using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatShop : MonoBehaviour
{
    public GameObject window;
    public UICat[] slots;
    public GameObject confirmation;
    public TMP_Text confirmTex;
    CatSO selected;
    static CatSO[] allCats;
    void Start()
    {
        if(allCats == null) allCats = Resources.LoadAll<CatSO>("Buyable");
        Refresh();
    }

    public void Refresh()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            CatSO cat = allCats[Random.RandomRange(0, allCats.Length)];
            slots[i].SetCatSO(cat);
            slots[i].SetOnClick(() =>
            {
                confirmation.SetActive(true);
                confirmTex.text = "Buy " + cat.CatName + " for " + cat.Cost + " food?";
                selected = cat;
            });

            if (cat.Cost > GameManager.gameState.GetFood())
            {
                slots[i].DisableClick();
            }
        }
    }

    public void ConfirmBuy()
    {
        GameManager.gameState.AddCat(selected.Clone(), true);
        GameManager.gameState.TryConsumeFood(selected.Cost);
        confirmation.SetActive(false);
        window.SetActive(false);
    }

}
