using UnityEngine;

[CreateAssetMenu(fileName = "PillarOfStrength", menuName = "ScriptableObjects/Abilities/PillarOfStrength")]
public class PillarOfStrength : Ability 
{

    private void OnEnable() 
    {
        abilityName = "Pillar Of Strength";
        description = "If this cat is in Befriend, all cats that train will permanently gain +X to all stats, where X is the number of cats in Train.";
    }

    public override bool IsActive(Cat cat)
    {
        GameObject trainObject = GameObject.Find("Train");
        if (trainObject == null) { return false; }

        TrainArea trainArea = trainObject.GetComponent<TrainArea>();
        if (trainArea == null) { return false; }

        if (cat.currArea.Equals("Conquer") && cat._catSO.Ability.abilityName == abilityName && trainArea.GetNumCats() >= 1)
        {
            return true;
        }

        return false;
    }
}