using UnityEngine;

[CreateAssetMenu(fileName = "ThreeMusketeers", menuName = "ScriptableObjects/Abilities/ThreeMusketeers")]
public class ThreeMusketeers : Ability 
{

    private void OnEnable() 
    {
        abilityName = "Three Musketeers";
        description = "If this cat is in Hunt with at least two other cats, double the amount of food gained when hunting.";
  
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

        if (cat.currArea.Equals("Hunt") && cat._catSO.Ability.abilityName == abilityName && huntArea.GetCats().Count >= 3)
        {
            return true;
        }

        return false;
    }
}