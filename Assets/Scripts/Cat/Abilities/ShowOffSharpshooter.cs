using UnityEngine;

[CreateAssetMenu(fileName = "ShowOffSharpshooter", menuName = "ScriptableObjects/Abilities/ShowOffSharpshooter")]
public class ShowOffSharpshooter : Ability 
{

    private void OnEnable() 
    {
        abilityName = "Showoff Sharpshooter";
        description = "If this cat is in Hunt and the colony contains more than 10 cats, triple this cat's hunting stat.";
    }

    public override int GetHuntingBuff(Cat cat)
    {
        int buff = ReceiveHuntingBuff(cat);
        if (IsActive(cat))
        {
            buff += ReceiveHuntingBuff(cat);
            buff += ReceiveHuntingBuff(cat);
            buff += cat.GetCatSO().Hunting;
            buff += cat.GetCatSO().Hunting;
        }

        return buff;
    }

    public override bool IsActive(Cat cat)
    {
        if (cat._catSO.Ability.abilityName == abilityName && cat.currArea == "Hunt" && GameManager.instance.GetCatInstances().Count > 10)
        {
            return true;
        }

        return false;
    }
}