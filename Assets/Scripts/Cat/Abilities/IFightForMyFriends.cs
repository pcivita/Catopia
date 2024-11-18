using UnityEngine;

[CreateAssetMenu(fileName = "IFightForMyFriends", menuName = "ScriptableObjects/Abilities/IFightForMyFriends")]
public class IFightForMyFriends : Ability 
{

    private void OnEnable() 
    {
        abilityName = "I Fight For My Friends";
        description = "This cat gains +3 Strength/+3 Health when in battle with at least one other cat.";
    }

    public override int GetHealthBuff(Cat cat)
    {
        GameObject conquerObject = GameObject.Find("Conquer");
        if (conquerObject == null)
        {
            Debug.Log("Could not find object.");
            return 0;
        }

        ConquerArea conquerArea = conquerObject.GetComponent<ConquerArea>();
        if (conquerArea == null) 
        { 
            Debug.Log("Could not find area.");
            return 0;
        }

        if (cat.currArea.Equals("Conquer") && conquerArea.GetCats().Count >= 2)
        {
            return 3;
        }

        return 0;
    }

    public override int GetStrengthBuff(Cat cat)
    {
        return GetHealthBuff(cat);
    }
}