using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class EventManager : MonoBehaviour
{
    public GameObject eventCanvas;
    public TMP_Text eventText;
    public Button eventChoice1;
    public Button eventChoice2;
    public Button eventDoneButton;

    List<Cat> catsPresent = new List<Cat>();
    delegate void EventFunction();
    List<EventFunction> possibleEvents;

    private void Start()
    {
        possibleEvents = new List<EventFunction>();
        possibleEvents.Add(Bees);
        possibleEvents.Add(RougeCat);
        /*
        possibleEvents.Add(LostHousecat);
        possibleEvents.Add(RiskyFood);
        possibleEvents.Add(FoodTheif);*/
    }

    public void PlayEvent()
    {
        eventChoice1.gameObject.SetActive(false);
        eventChoice2.gameObject.SetActive(false);
        eventDoneButton.gameObject.SetActive(false);

        catsPresent.Clear();
        foreach(var cat in GameManager.instance.catInstances)
        {
            if (!cat.inArea) catsPresent.Add(cat);
        }

        eventCanvas.SetActive(true);
        EventFunction func = possibleEvents[UnityEngine.Random.RandomRange(0, possibleEvents.Count)];
        func();
    }

    //event done button calls this, youprobs dont have to call this in the script.
    public void EventDone()
    {
        eventCanvas.SetActive(false);
    }

    Cat GetRandomPresentCat()
    {
        return catsPresent[UnityEngine.Random.RandomRange(0, catsPresent.Count)];
    }

    void CreateChoice(Button button, string buttonText, Action onClick)
    {
        button.onClick.RemoveAllListeners();
        button.gameObject.SetActive(true);
        button.GetComponentInChildren<TMP_Text>().text = buttonText;
        button.onClick.AddListener(()=> {
            button.gameObject.SetActive(false);
            onClick();
        }
        );
    }

    float Randf(float from, float to)
    {
        return UnityEngine.Random.RandomRange(from, to);
    }

    void AllowExitEvent()
    {
        eventDoneButton.gameObject.SetActive(true);
    }

    void Bees(){
        Cat cat = GetRandomPresentCat();

        eventText.text = cat._catSO.CatName + " bumped into a bee hive, oh no! It seems like you would need a lot of HEALTH to run away from bees...";

        CreateChoice(eventChoice1, "try to get honey. success rate: " + cat._catSO.Health/10f, () => {
            if (Randf(0, 10) < cat._catSO.Health){
                eventText.text = "Success! " + cat._catSO.CatName + "gets the honey. You got 2 food.";
                GameManager.instance.gameState.AddFood(2);
                AllowExitEvent();
            }
            else{
                eventText.text = "Oh no! " + cat._catSO.CatName + "got injured badly. They lost 2 HUNTING.";
                cat._catSO.Hunting -= 2;
                AllowExitEvent();
            }
        });

        CreateChoice(eventChoice2, "run!", () => {
                eventText.text = cat._catSO.CatName + "got a bit stung. They lost 1 HUNTING.";
                cat._catSO.Hunting -= 1;
                AllowExitEvent();
        });
    }

    //TODO randomly generate and add a cat on success.
    void RougeCat()
    {
        Cat cat = GetRandomPresentCat();

        eventText.text = cat._catSO.CatName + " crossed paths with a rouge cat! It looks dangerous...";

        CreateChoice(eventChoice1, "Approach and try to befriend. Success rate: 50%", () => {
            if (Randf(0, 1) < 0.5){
                eventText.text = "Success! " + cat._catSO.CatName + " befriends the rouge cat. Welcome home, BLAH";
                AllowExitEvent();
            }
            else
            {
                eventText.text = "Oh no! " + cat._catSO.CatName + " got injured badly. They lost 2 HEALTH.";
                cat._catSO.Health -= 2;
                AllowExitEvent();
            }
        });

        CreateChoice(eventChoice2, "run!", () => {
            eventText.text = cat._catSO.CatName + " plays it safe and avoids them.";
            AllowExitEvent();
        });
    }

    /*
    void LostHousecat()
    {

    }

    void RiskyFood()
    {

    }

    void FoodTheif()
    {

    }*/
}
