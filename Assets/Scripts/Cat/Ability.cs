using UnityEngine;

public abstract class Ability : ScriptableObject 
{
    
    public string abilityName;
    public string description;


    public virtual int GetHealthBuff(Cat cat) { return 0; }
    public virtual int GetStrengthBuff(Cat cat) { return 0; }
    public virtual int GetHuntingBuff(Cat cat) { return 0; }
}