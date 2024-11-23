using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public UICat template;
    public TMP_Text chooseButtonText;
    public Button chooseButton;
    public TMP_Text gameOverInfo;

    List<GameObject> catButtons = new List<GameObject>();

    CatSO keep;
    // Start is called before the first frame update
    void Start()
    {
        //create grid of cats to choose to keep
        foreach(CatSO cat in GameManager.gameState.GetCats())
        {
            UICat catButton = Instantiate(template.gameObject, template.transform.parent).GetComponent<UICat>();
            catButton.SetCatSO(cat);
            catButton.gameObject.SetActive(true);
            catButtons.Add(catButton.gameObject);
            catButton.SetOnClick(() => {
                keep = cat.Clone();
                chooseButtonText.text = "Restart with " + keep.CatName;
            });
        }

        //default selection
        keep = GameManager.gameState.GetCats()[0];
        chooseButtonText.text = "Restart with " + keep.CatName;
        template.gameObject.SetActive(false);

        //what happens when you click the button to continue
        chooseButton.onClick.RemoveAllListeners();
        chooseButton.onClick.AddListener(() =>
        {
            foreach(GameObject go in catButtons) Destroy(go);
            catButtons.Clear();
            //new gamestate
            GameManager.ResetGame();
            //replace one cat
            GameManager.gameState.AddCat(keep, false);
            //GameManager.gameState.KillCat(GameManager.gameState.GetCats()[0]);
            //load up scene
            SceneManager.LoadScene("ColonyScene");
        });
    }

    public void SetGameoverMessage(string msg)
    {
        gameOverInfo.text = msg;
        LayoutRebuilder.ForceRebuildLayoutImmediate(gameOverInfo.rectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(gameOverInfo.transform.parent.GetComponent<RectTransform>());

        //Canvas.ForceUpdateCanvases();
    }
}
