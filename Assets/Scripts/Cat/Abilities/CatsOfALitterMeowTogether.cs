using UnityEngine;

[CreateAssetMenu(fileName = "CatsOfALitterMeowTogether", menuName = "ScriptableObjects/Abilities/CatsOfALitterMeowTogether")]
public class CatsOfALitterMeowTogether : Ability 
{

    private void OnEnable() 
    {
        abilityName = "Cats Of A Litter, Meow Together";
        description = "If this cat is in the same area as another cat with the ability 'Cats Of A Litter, Meow Together', double this cat's stats.";
    }

    public override int GetHealthBuff(Cat cat)
    {
        int buff = ReceiveHealthBuff(cat);
        if (IsActive(cat))
        {
            buff += ReceiveHealthBuff(cat);
            buff += cat.GetCatSO().Health;
        }

        return buff;
    }

    public override int GetStrengthBuff(Cat cat)
    {
        int buff = ReceiveStrengthBuff(cat);
        if (IsActive(cat))
        {
            buff += ReceiveStrengthBuff(cat);
            buff += cat.GetCatSO().Strength;
        }

        return buff;
    }

    public override int GetHuntingBuff(Cat cat)
    {
        int buff = ReceiveHuntingBuff(cat);
        if (IsActive(cat))
        {
            buff += ReceiveHuntingBuff(cat);
            buff += cat.GetCatSO().Hunting;
        }

        return buff;
    }

    public override bool IsActive(Cat cat)
    {
        if (cat._catSO.Ability.abilityName == abilityName && cat.currArea != "None")
        {
            foreach (Cat otherCat in GameManager.instance.GetCatInstances())
            {
                if (otherCat != cat && otherCat.GetAbility().abilityName == abilityName)
                {
                    if (otherCat.currArea.Equals(cat.currArea))
                    {
                        return true;
                    }
                }        
            }
        }

        return false;
    }
}