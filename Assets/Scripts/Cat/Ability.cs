using UnityEngine;

public abstract class Ability : ScriptableObject 
{
    
    public string abilityName;
    public string description;
    public Sprite icon;


    public virtual int GetHealthBuff(Cat cat) { return ReceiveHealthBuff(cat); }
    public virtual int GetStrengthBuff(Cat cat) { return ReceiveStrengthBuff(cat); }
    public virtual int GetHuntingBuff(Cat cat) { return ReceiveHuntingBuff(cat); }

    public virtual int GiveHealthBuff(Cat giver, Cat receiver) { return 0; }
    public virtual int GiveStrengthBuff(Cat giver, Cat receiver) { return 0; }
    public virtual int GiveHuntingBuff(Cat giver, Cat receiver) { return 0; }

    public virtual int ReceiveHealthBuff(Cat cat) 
    { 
        int buff = 0;
        foreach(Cat otherCat in GameManager.instance.GetCatInstances())
        {
            if (otherCat != cat)
            {
                Ability otherCatAbility = otherCat.GetAbility();
                buff += otherCatAbility.GiveHealthBuff(otherCat, cat);
            }
        }

        return buff;
    }

    public virtual int ReceiveStrengthBuff(Cat cat)
    { 
        int buff = 0;
        foreach(Cat otherCat in GameManager.instance.GetCatInstances())
        {
            if (otherCat != cat)
            {
                Ability otherCatAbility = otherCat.GetAbility();
                buff += otherCatAbility.GiveStrengthBuff(otherCat, cat);
            }
        }

        return buff;
    }

    public virtual int ReceiveHuntingBuff(Cat cat)
    { 
        int buff = 0;
        foreach(Cat otherCat in GameManager.instance.GetCatInstances())
        {
            if (otherCat != cat)
            {
                Ability otherCatAbility = otherCat.GetAbility();
                buff += otherCatAbility.GiveHuntingBuff(otherCat, cat);
            }
        }

        return buff;
    }

    public virtual bool IsActive(Cat cat) { return false; }
    public string GetName() { return abilityName; }
    public string GetDescription() { return description; }
}