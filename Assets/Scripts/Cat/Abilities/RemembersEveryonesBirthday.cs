
using UnityEngine;

[CreateAssetMenu(fileName = "RemembersEveryonesBirthday", menuName = "ScriptableObjects/Abilities/RemembersEveryonesBirthday")]
public class RemembersEveryonesBirthday : Ability 
{

    private void OnEnable() 
    {
        abilityName = "Remembers Everyone's Birthday";
        description = "All other cats gain +2 to all stats.";        
    }

    public override int GiveHealthBuff(Cat giver, Cat receiver)
    {
        int buff = 0;
        if (IsActive(giver))
        { 
            buff += 2;
        }

        return buff;
    }

    public override int GiveStrengthBuff(Cat giver, Cat receiver)
    {
        int buff = 0;
        if (IsActive(receiver))
        {
            buff += 2;
        }

        return buff;
    }

    public override int GiveHuntingBuff(Cat giver, Cat receiver)
    {
        int buff = 0;
        if (IsActive(receiver))
        {
            buff += 2;
        }

        return buff;
    }

    public override bool IsActive(Cat cat)
    {
        return true;
    }
}