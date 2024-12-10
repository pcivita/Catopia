using UnityEngine;

[CreateAssetMenu(fileName = "SmellsGood", menuName = "ScriptableObjects/Abilities/SmellsGood")]
public class SmellsGood : Ability 
{

    private void OnEnable() 
    {
        abilityName = "Smells Good";
        description = "All other cats in the same area as this cat gain +3 to all stats.";        
    }

    public override int GiveHealthBuff(Cat giver, Cat receiver)
    {
        int buff = 0;
        if (IsActive(giver))
        { 
            if (giver.GetCurrArea().Equals(receiver.GetCurrArea()))
            {
                buff += 3;
            }
        }

        return buff;
    }

    public override int GiveStrengthBuff(Cat giver, Cat receiver)
    {
        int buff = 0;
        if (IsActive(receiver))
        {
            if (giver.GetCurrArea().Equals(receiver.GetCurrArea()))
            {
                buff += 3;
            }
        }

        return buff;
    }

    public override int GiveHuntingBuff(Cat giver, Cat receiver)
    {
        int buff = 0;
        if (IsActive(receiver))
        {
            if (giver.GetCurrArea().Equals(receiver.GetCurrArea()))
            {
                buff += 3;
            }
        }

        return buff;
    }

    public override bool IsActive(Cat cat)
    {
        GameObject conquerObject = GameObject.Find("Conquer");
        if (conquerObject == null) { return false; }
        ConquerArea conquerArea = conquerObject.GetComponent<ConquerArea>();
        if (conquerArea == null) { return false; }
        if (cat.GetCurrArea().Equals("Conquer") && conquerArea.GetNumCats() >= 2)
        {
            return true;
        }

        GameObject huntObject = GameObject.Find("Hunt");
        if (huntObject == null) { return false; }
        HuntArea huntArea = huntObject.GetComponent<HuntArea>();
        if (huntArea == null) { return false; }
        if (cat.GetCurrArea().Equals("Hunt") && huntArea.GetNumCats() >= 2)
        {
            return true;
        }
 
        GameObject trainObject = GameObject.Find("Train");
        if (trainObject == null) { return false; }
        TrainArea trainArea = trainObject.GetComponent<TrainArea>();
        if (trainArea == null) { return false; }
        if (cat.GetCurrArea().Equals("Train") && trainArea.GetNumCats() >= 2)
        {
            return true;
        }

        return false;
    }
}