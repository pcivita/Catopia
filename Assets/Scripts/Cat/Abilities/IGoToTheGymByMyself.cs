using UnityEngine;

[CreateAssetMenu(fileName = "IGoToTheGymByMyself", menuName = "ScriptableObjects/Abilities/IGoToTheGymByMyself")]
public class IGoToTheGymByMyself : Ability 
{

    private void OnEnable() 
    {
        abilityName = "I Go To The Gym By Myself";
        description = "If this cat is in Train by itself, triple any stat gains it receives on Train.";
    }

    public override bool IsActive(Cat cat)
    {
        GameObject trainObject = GameObject.Find("Train");
        if (trainObject == null) { return false; }

        TrainArea trainArea = trainObject.GetComponent<TrainArea>();
        if (trainArea == null) { return false; }

        if (cat.currArea.Equals("Train") && cat._catSO.Ability.abilityName == abilityName && trainArea.GetNumCats() == 1)
        {
            return true;
        }

        return false;
    }
}