using UnityEngine;

[CreateAssetMenu(fileName = "ThreeMusketeers", menuName = "ScriptableObjects/Abilities/ThreeMusketeers")]
public class ThreeMusketeers : Ability 
{

    private void OnEnable() 
    {
        abilityName = "Three Musketeers";
        description = "If this cat is in Hunt with at least two other cats, double the amount of food gained when hunting.";
    }
}