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
        GameObject huntObject = GameObject.Find("Hunt");
        if (huntObject == null)
        {
            Debug.Log("Could not find object.");
            return 0;
        }

        HuntArea huntArea = huntObject.GetComponent<HuntArea>();
        if (huntArea == null) 
        { 
            Debug.Log("Could not find area.");
            return 0;
        }

        if (cat.currArea.Equals("Conquer"))
        {
            return huntArea.GetCats().Count;
        }

        return 0;
    }

    public override int GetStrengthBuff(Cat cat)
    {
        return GetHealthBuff(cat);
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