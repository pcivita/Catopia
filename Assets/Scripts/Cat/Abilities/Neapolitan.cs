using UnityEngine;

[CreateAssetMenu(fileName = "Neapolitan", menuName = "ScriptableObjects/Abilities/Neapolitan")]
public class Neapolitan : Ability 
{

    private void OnEnable() 
    {
        abilityName = "Neapolitan";
        description = "If there is at least one cat in each area, this cat gains +5 to all stats.";
    }

    public override int GetHealthBuff(Cat cat)
    {
        int buff = ReceiveHealthBuff(cat);
        if (IsActive(cat)) {
            buff += 5;
        }

        return buff;
    }

    public override int GetStrengthBuff(Cat cat)
    {
        int buff = ReceiveStrengthBuff(cat);
        if (IsActive(cat)) {
            buff += 5;
        }

        return buff;
    }

    public override int GetHuntingBuff(Cat cat)
    {
        int buff = ReceiveHuntingBuff(cat);
        if (IsActive(cat)) {
            buff += 5;
        }

        return buff;
    }

    public override bool IsActive(Cat cat)
    {
        GameObject conquerObject = GameObject.Find("Conquer");
        if (conquerObject == null) { return false; }

        ConquerArea conquerArea = conquerObject.GetComponent<ConquerArea>();
        if (conquerArea == null) { return false; }

        GameObject huntObject = GameObject.Find("Hunt");
        if (huntObject == null) { return false; }

        HuntArea huntArea = huntObject.GetComponent<HuntArea>();
        if (huntArea == null) { return false; }
    
        GameObject trainObject = GameObject.Find("Train");
        if (trainObject == null) { return false; }

        TrainArea trainArea = trainObject.GetComponent<TrainArea>();
        if (trainArea == null) { return false; }

        if (cat._catSO.Ability.abilityName == this.abilityName && conquerArea.GetNumCats() >= 1 && huntArea.GetNumCats() >= 1  && trainArea.GetNumCats() >= 1)
        {
            return true;
        }

        return false;
    }
}