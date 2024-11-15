using UnityEngine;

[CreateAssetMenu(fileName = "CatSO", menuName = "ScriptableObjects/CatSO")]
public class CatSO : ScriptableObject{
    public string CatName;
    public int Attack;
    public int Health;
    public int Hunting;

    public Sprite Accessory;
    public Sprite Pattern;
    public Color32 patternColor;
    public Color32 bodyColor;
}
