using UnityEngine;
[CreateAssetMenu(fileName = "CatSO", menuName = "ScriptableObjects/CatSO")]
public class CatSO : ScriptableObject
{
    public string CatName;
    public int Attack;
    public int Health;
    public int Hunting;
    public Sprite Sprite;
    
}
