using UnityEngine;

[CreateAssetMenu(fileName = "IFightForMyFriends", menuName = "ScriptableObjects/Abilities/IFightForMyFriends")]
public class IFightForMyFriends : Ability 
{

    private void OnEnable() 
    {
        abilityName = "I Fight For My Friends";
        description = "This cat gains +3 Strength/+3 Health when in battle with at least one other cat.";
        
    }

    public override int GetHealthBuff(Cat cat)
    {
        int buff = ReceiveHealthBuff(cat);
        if (IsActive(cat))
        {
            buff += 3;
        }

        return buff;
    }

    public override int GetStrengthBuff(Cat cat)
    {
        int buff = ReceiveStrengthBuff(cat);
        if (IsActive(cat))
        {
            buff += 3;
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