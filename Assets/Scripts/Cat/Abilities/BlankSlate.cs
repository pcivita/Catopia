using UnityEngine;

[CreateAssetMenu(fileName = "BlankSlate", menuName = "ScriptableObjects/Abilities/BlankSlate")]
public class BlankSlate : Ability 
{

    private void OnEnable() 
    {
        abilityName = "Blank Slate";
        description = "This ability does nothing.";
    }
}