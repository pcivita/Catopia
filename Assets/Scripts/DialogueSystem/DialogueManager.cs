using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public Queue<string> sentences;

    public TMP_Text nameText;
    public TMP_Text dialogueText;
    
    public float speed;
    private bool inSentence;
    void Awake()
    {
        // Ensure there is only one instance of this object
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist through scene changes
        }
        else
        {
            Debug.LogWarning("Multiple DialogueManager instances found. Destroying duplicate.");
            Destroy(gameObject);
        }
    }
    void Start()
    {
        speed = 0.05f;
        sentences = new Queue<string>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (inSentence)
            {
                speed = 0.01f;
            }
            else
            {
                DisplayNextSentence();
            }

        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        inSentence = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(speed);
        }
        inSentence = false;
    }

    void EndDialogue()
    {
        Debug.Log("End dialogue");
        
        
    }
}
