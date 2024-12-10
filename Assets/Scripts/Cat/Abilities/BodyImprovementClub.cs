using UnityEngine;

[CreateAssetMenu(fileName = "BodyImprovementClub", menuName = "ScriptableObjects/Abilities/BodyImprovementClub")]
public class BodyImprovementClub : Ability 
{

    private void OnEnable() 
    {
        abilityName = "Body Improvement Club";
        description = "This cat gains +X to all stats, where X is the number of cats in Train.";
    }

    public override int GetHealthBuff(Cat cat)
    {
        int buff = ReceiveHealthBuff(cat);
        if (IsActive(cat))
        {
            GameObject trainObject = GameObject.Find("Train");
            TrainArea trainArea = trainObject.GetComponent<TrainArea>();
            buff += trainArea.GetCats().Count;
        }

        return buff;
    }

    public override int GetStrengthBuff(Cat cat)
    {
        int buff = ReceiveStrengthBuff(cat);
        if (IsActive(cat))
        {
            GameObject trainObject = GameObject.Find("Train");
            TrainArea trainArea = trainObject.GetComponent<TrainArea>();
            buff += trainArea.GetCats().Count;
        }

        return buff;
    }

    public override int GetHuntingBuff(Cat cat)
    {
        int buff = ReceiveHuntingBuff(cat);
        if (IsActive(cat))
        {
            GameObject trainObject = GameObject.Find("Train");
            TrainArea trainArea = trainObject.GetComponent<TrainArea>();
            buff += trainArea.GetCats().Count;
        }

        return buff;
    }

    public override bool IsActive(Cat cat)
    {
        GameObject trainObject = GameObject.Find("Train");
        if (trainObject == null) 
        {
            return false;
        }

        TrainArea trainArea = trainObject.GetComponent<TrainArea>();
        if (trainArea == null) 
        {
            return false;
        }

        if (!cat.currArea.Equals("None") && cat._catSO.Ability.abilityName == abilityName && trainArea.GetNumCats() > 0)
        {
            return true;
        }

        return false;
    }
}