using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public void playGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("ColonyScene");
    }
}
