using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FurnitureSlot : MonoBehaviour
{
    public Image image;
    public TMP_Text costText;
    public Button button;
    public Furniture furniture;
    public void SetFurniture(bool interactable, Furniture f, Action a)
    {
        button.interactable = interactable;
        furniture = f;
        image.sprite = f.Sprite;
        costText.text = f.Cost.ToString();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => a());
    }
}
