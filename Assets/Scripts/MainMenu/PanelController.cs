using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{
    // UI Components
    public TMP_Text contentText;      // Assign in Inspector
    public Image contentImage;        // Assign in Inspector
    
    public TMP_Text nextButtonText;
   // Assign in Inspector

    // Arrays of texts and sprites
    [TextArea(3, 10)]
    public string[] texts;            // Populate in Inspector
    public Sprite[] images;           // Populate in Inspector

    // Scene to load at the end
    public string sceneToLoad = "NextScene"; // Set the name of the scene to load

    private int currentIndex = 0;

    void Start()
    {
        // Initialize UI with the first state
        UpdateContent();
    }

    void UpdateContent()
    {
        // Update text and image based on currentIndex
        if (currentIndex < texts.Length)
        {
            contentText.text = texts[currentIndex];
        }

        if (currentIndex < images.Length)
        {
            contentImage.sprite = images[currentIndex];
        }

        // Check if this is the last state
        if (currentIndex == texts.Length - 1 || currentIndex == images.Length - 1)
        {
            nextButtonText.text = "Get Started!";
        }
    }

    public void ClickButton()
    {
        currentIndex++;

        // Check if we have reached the end
        if (currentIndex >= texts.Length || currentIndex >= images.Length)
        {
            // Load the next scene
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // Update the content for the next state
            UpdateContent();
        }
    }
}