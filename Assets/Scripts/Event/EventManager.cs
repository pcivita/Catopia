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
        possibleEvents.Add(Uneventful);
        possibleEvents.Add(SunsetFriendship);
        /*
        possibleEvents.Add(LostHousecat);
        possibleEvents.Add(RiskyFood);
        possibleEvents.Add(FoodTheif);*/
        eventCanvas.SetActive(false);
    }

    public void PlayEvent(Action onEnd)
    {
        Debug.Log("PlayEvent");
        eventChoice1.gameObject.SetActive(false);
        eventChoice2.gameObject.SetActive(false);
        eventDoneButton.gameObject.SetActive(false);

        eventDoneButton.onClick.RemoveAllListeners();
        eventDoneButton.onClick.AddListener(()=>
        {
            onEnd();
            eventCanvas.SetActive(false);
        });

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


    Cat GetRandomPresentCat()
    {
        if (catsPresent.Count == 0) return null;
        return catsPresent[UnityEngine.Random.RandomRange(0, catsPresent.Count)];
    }

    void CreateChoice(Button button, string buttonText, Action onClick)
    {
        button.onClick.RemoveAllListeners();
        button.gameObject.SetActive(true);
        button.GetComponentInChildren<TMP_Text>().text = buttonText;
        button.onClick.AddListener(()=> {
            eventChoice1.gameObject.SetActive(false);
            eventChoice2.gameObject.SetActive(false);
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

    void Uneventful()
    {
        AllowExitEvent();
        Cat cat = GetRandomPresentCat();
        if (cat == null)
        {
            eventText.text = "At home, it's quiet tonight. Everyone is busy.";
            return;
        }
        eventText.text = cat._catSO.CatName + " stays home, and chases a butterfly.";
    }

    void SunsetFriendship()
    {
        if (catsPresent.Count < 2)
        {
            eventText.text = "The sunset is beautiful today. If only there were two cats at home to enjoy it together.";
            AllowExitEvent();
            return;
        }
        Cat cat1 = GetRandomPresentCat();
        Cat cat2 = GetRandomPresentCat();
        while(cat2==cat1) cat2 = GetRandomPresentCat();

        eventText.text = "The sunset is beautiful today. " + cat1._catSO.CatName + " and " + cat2._catSO.CatName + " have a heart-to-heart watching the clouds. Their HEALTH increased by 1!"; ;
        cat1._catSO.Health += 1;
        cat2._catSO.Health += 1;
        AllowExitEvent();
    }

    void Bees(){
        Cat cat = GetRandomPresentCat();
        if(cat == null)
        {
            eventText.text = "Nobody was at home to protect the food! You lost 1 food.";
            GameManager.gameState.TryConsumeFood(1);
            AllowExitEvent();
            return;
        }

        eventText.text = cat._catSO.CatName + " bumped into a bee hive, oh no!";

        CreateChoice(eventChoice1, "try to get honey. success rate: 50%", () => {
            if (Randf(0, 1) < 0.5f){
                eventText.text = "Success! " + cat._catSO.CatName + "gets the honey. You got 5 food.";
                GameManager.gameState.AddFood(2);
                AllowExitEvent();
            }
            else{
                eventText.text = "Oh no! " + cat._catSO.CatName + "got injured badly. They lost 2 HUNTING.";
                cat._catSO.Hunting -= 2;
                AllowExitEvent();
            }
        });

        CreateChoice(eventChoice2, "run!", () => {
                eventText.text = cat._catSO.CatName + "got a bit stung, but they're fine!";
                AllowExitEvent();
        });
    }

    //TODO randomly generate and add a cat on success.
    void RougeCat()
    {
        Cat cat = GetRandomPresentCat();
        if (cat == null)
        {
            eventText.text = "Nobody was at home to protect the food! You lost 1 food.";
            GameManager.gameState.TryConsumeFood(1);
            AllowExitEvent();
            return;
        }

        eventText.text = cat._catSO.CatName + " crossed paths with a rouge cat! Is it a friend or a foe?";

        CreateChoice(eventChoice1, "Approach and try to befriend. Success rate: 33%", () => {
            if (Randf(0, 1) < 0.33f){
                CatSO newCat = GameManager.GetRandomDefaultCat();
                GameManager.gameState.AddCat(newCat);
                eventText.text = "Success! " + cat._catSO.CatName + " befriends the rouge cat. Welcome home, +" +newCat.CatName+"!";
                AllowExitEvent();
            }
            else
            {
                eventText.text = "Oh no! " + cat._catSO.CatName + " got injured badly. They lost 1 HEALTH.";
                cat._catSO.Health -= 1;
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
