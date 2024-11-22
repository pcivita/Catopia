using UnityEngine;
using System.Collections.Generic;

public class PlayerLog : MonoBehaviour
{
    private List<string> eventLog = new List<string>();
    private string guiText = "";

    public static PlayerLog instance;

    public int maxLines = 10;

    // Style customization variables
    public Color textColor = Color.white; // Default text color
    public Font customFont; // Optional custom font
    public int fontSize = 14; // Font size
    public Vector2 position = new Vector2(10, 300); // Position on screen
    public Vector2 size = new Vector2(400, 200); // Width and height of the GUI area
    public float opacity = 1.0f; // Opacity (0 = transparent, 1 = fully opaque)

    private GUIStyle textStyle; // GUI style for customization

    void Start()
    {

        instance = this;
        // Initialize the GUIStyle
        textStyle = new GUIStyle();
        textStyle.normal.textColor = textColor;
        textStyle.font = customFont;
        textStyle.fontSize = fontSize;
        textStyle.wordWrap = true; // Allow text to wrap within the area
    }

    void OnGUI()
    {
        // Define the GUI area
        Rect guiRect = new Rect(position.x, Screen.height - position.y, size.x, size.y);

        // Define the outline color and thickness
        Color outlineColor = Color.black; // Black outline
        int outlineThickness = 4; // Thickness of the outline

        // Save the current GUI color
        Color originalColor = GUI.color;

        // Draw the outline (black rectangle slightly larger than the GUI area)
        GUI.color = outlineColor;
        GUI.DrawTexture(new Rect(guiRect.x - outlineThickness, guiRect.y - outlineThickness, guiRect.width + 2 * outlineThickness, guiRect.height + 2 * outlineThickness), Texture2D.whiteTexture);

        // Draw the black background rectangle
        Color backgroundColor = Color.black; // Black background
        backgroundColor.a = opacity; // Apply transparency from opacity variable
        GUI.color = backgroundColor;
        GUI.DrawTexture(guiRect, Texture2D.whiteTexture);

        // Restore the original GUI color
        GUI.color = originalColor;

        // Render the text
        GUI.Label(guiRect, guiText, textStyle);
    }



    public void AddEvent(string eventString)
    {
        eventLog.Add("Caleb Cuddlepaw will train today!");

        if (eventLog.Count > maxLines)
        {
            eventLog.RemoveAt(0);
        }

        guiText = string.Join("\n", eventLog);
    }
}