
using UnityEngine;

[CreateAssetMenu(fileName = "AllForOne", menuName = "ScriptableObjects/Abilities/AllForOne")]
public class AllForOne : Ability 
{

    private void OnEnable() 
    {
        abilityName = "All For One";
        description = "If this cat is in Befriend, gain +10 Strength and +10 Health. All other cats in Befriend gain -5 Strength and -5 Health.";        
    }

    public override int GiveHealthBuff(Cat giver, Cat receiver)
    {
        int buff = 0;
        if (IsActive(giver))
        {
            if (receiver.GetCurrArea() == "Conquer")
            {
                buff += -5;
            }
        }

        return buff;
    }

    public override int GiveStrengthBuff(Cat giver, Cat receiver)
    {
        int buff = 0;
        if (IsActive(giver))
        {
            if (receiver.GetCurrArea() == "Conquer")
            {
                buff += -5;
            }
        }

        return buff;
    }

    public override int GetHealthBuff(Cat cat)
    {
        int buff = ReceiveHealthBuff(cat);
        if (IsActive(cat))
        {
            buff += 10;
        }

        return buff;
    }

    public override int GetStrengthBuff(Cat cat)
    {
        int buff = ReceiveStrengthBuff(cat);
        if (IsActive(cat))
        {
            buff += 10;
        }

        return buff;
    }

    public override bool IsActive(Cat cat)
    {
        GameObject conquerObject = GameObject.Find("Conquer");
        if (conquerObject == null)
        {
            return false;
        }

        ConquerArea conquerArea = conquerObject.GetComponent<ConquerArea>();
        if (conquerArea == null) 
        { 
            return false;
        }

        if (cat._catSO.Ability.abilityName == this.abilityName && cat.currArea.Equals("Conquer"))
        {
            return true;
        }

        return false;
    }
}