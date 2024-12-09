using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FurnitureStore : MonoBehaviour
{
    Furniture[] buyable;
    public GameObject confirm;
    public Button yesButton;
    public TMP_Text confirmText;

    void Start(){
        confirm.SetActive(false);
        buyable = Resources.LoadAll<Furniture>("Furniture");
        int i = 0;
        foreach(FurnitureSlot f in GetComponentsInChildren<FurnitureSlot>())
        {
            if (i < buyable.Length)
            {
                Furniture furniture = buyable[i];
                f.SetFurniture(furniture.Cost<= GameManager.gameState.GetFood(), buyable[i],()=> {
                    confirm.SetActive(true);
                    confirmText.text = "Buy " + furniture.Name + " for " + furniture.Cost + " food?";
                    yesButton.onClick.RemoveAllListeners();
                    yesButton.onClick.AddListener(() =>
                    {
                        furniture.Construct();
                        confirm.SetActive(false);
                        gameObject.SetActive(false);
                        GameManager.gameState.TryConsumeFood(furniture.Cost);
                        GameManager.furniture.Add(furniture);
                    });
                 });
                i++;
            }
            else
            {
                f.gameObject.SetActive(false);
            }
        }
    }
}
