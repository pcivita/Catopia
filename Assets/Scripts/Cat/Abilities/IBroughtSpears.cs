using UnityEngine;

[CreateAssetMenu(fileName = "IBroughtSpears", menuName = "ScriptableObjects/Abilities/IBroughtSpears")]
public class IBroughtSpears : Ability 
{

    private void OnEnable() 
    {
        abilityName = "I Brought Spears";
        description = "If this cat is in Hunt, all other cats in Hunt gain +5 Hunting.";        
    }

    public override int GiveHuntingBuff(Cat giver, Cat receiver)
    {
        int buff = 0;
        if (IsActive(receiver))
        {
            if (giver.GetCurrArea().Equals(receiver.GetCurrArea()))
            {
                buff += 5;
            }
        }

        return buff;
    }

    public override bool IsActive(Cat cat)
    {
        GameObject huntObject = GameObject.Find("Hunt");
        if (huntObject == null) { return false; }
        HuntArea huntArea = huntObject.GetComponent<HuntArea>();
        if (huntArea == null) { return false; }
        if (cat.GetCurrArea().Equals("Hunt") && huntArea.GetNumCats() >= 2)
        {
            return true;
        }
 
        return false;
    }
}