using UnityEngine;

[CreateAssetMenu(fileName = "MyFaceIsScary", menuName = "ScriptableObjects/Abilities/MyFaceIsScary")]
public class MyFaceIsScary : Ability 
{

    private void OnEnable() 
    {
        abilityName = "My Face Is Scary";
        description = "If this cat is in Befriend, halve the stats of all other friendly cats in Befriend.";        
    }

    public override bool IsActive(Cat cat)
    {
        GameObject conquerObject = GameObject.Find("Conquer");
        if (conquerObject == null)
        {
            return false;
        }

        ConquerArea conquerArea = conquerObject.GetComponent<ConquerArea>();
        if (conquerArea == null) 
        { 
            return false;
        }

        if (cat._catSO.Ability.abilityName == this.abilityName && cat.currArea.Equals("Conquer") && conquerArea.GetNumCats() >= 2)
        {
            return true;
        }

        return false;
    }
}