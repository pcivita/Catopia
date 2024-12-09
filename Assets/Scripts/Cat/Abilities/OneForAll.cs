using UnityEngine;

[CreateAssetMenu(fileName = "OneForAll", menuName = "ScriptableObjects/Abilities/OneForAll")]
public class OneForAll : Ability 
{

    private void OnEnable() 
    {
        abilityName = "One For All";
        description = "If this cat is in Befriend, all other cats in Befriend gain +5 Strength and +5 Health.";        
    }

    public override int GiveHealthBuff(Cat giver, Cat receiver)
    {
        int buff = 0;
        if (IsActive(giver))
        { 
            if (receiver.GetCurrArea() == "Conquer")
            {
                buff += 5;
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
                buff += 5;
            }
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

        if (cat._catSO.Ability.abilityName == this.abilityName && cat.currArea.Equals("Conquer") && conquerArea.GetNumCats() >= 2)
        {
            return true;
        }

        return false;
    }
}