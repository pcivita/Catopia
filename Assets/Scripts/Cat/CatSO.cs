using UnityEngine;

[CreateAssetMenu(fileName = "CatSO", menuName = "ScriptableObjects/CatSO")]
public class CatSO : ScriptableObject
{
    public string CatName;
    public int Strength;
    public int Health;
    public int Hunting;
    public int Cost;
    public Ability Ability;

    public Sprite Accessory;
    public Sprite Pattern;
    
    public Color32 patternColor;
    public Color32 bodyColor;

    public CatSO Clone()
    {
        CatSO newCat = CreateInstance<CatSO>();
        newCat.CatName = CatName;
        newCat.Strength = Strength;
        newCat.Health = Health;
        newCat.Hunting = Hunting;
        newCat.Cost = Cost;
        newCat.Ability = Ability;

        newCat.Accessory = Accessory;
        newCat.Pattern = Pattern;
        newCat.Health = Health;
        newCat.patternColor = patternColor;
        newCat.bodyColor = bodyColor;

        return newCat;
    }
}