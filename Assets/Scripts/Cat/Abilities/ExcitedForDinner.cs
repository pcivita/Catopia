using UnityEngine;

[CreateAssetMenu(fileName = "ExcitedForDinner", menuName = "ScriptableObjects/Abilities/ExcitedForDinner")]
public class ExcitedForDinner : Ability 
{

    private void OnEnable() 
    {
        abilityName = "Excited For Dinner";
        description = "Gains +X Strength/+X Health in battle, where X is the number of cats in Hunt.";
    }

    public override int GetHealthBuff(Cat cat)
    {
        int buff = ReceiveHealthBuff(cat);
        if (IsActive(cat))
        {
            GameObject huntObject = GameObject.Find("Hunt");
            HuntArea huntArea = huntObject.GetComponent<HuntArea>();
            buff += huntArea.GetCats().Count;
        }

        return buff;
    }

    public override int GetStrengthBuff(Cat cat)
    {
        int buff = ReceiveStrengthBuff(cat);
        if (IsActive(cat))
        {
            GameObject huntObject = GameObject.Find("Hunt");
            HuntArea huntArea = huntObject.GetComponent<HuntArea>();
            buff += huntArea.GetCats().Count;
        }

        return buff;
    }

    public override bool IsActive(Cat cat)
    {
        GameObject huntObject = GameObject.Find("Hunt");
        if (huntObject == null) 
        {
            return false;
        }

        HuntArea huntArea = huntObject.GetComponent<HuntArea>();
        if (huntArea == null) 
        {
            return false;
        }

        if (cat.currArea.Equals("Conquer") && cat._catSO.Ability.abilityName == abilityName && huntArea.GetCats().Count > 0)
        {
            return true;
        }

        return false;
    }
}