using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region VARIABLES
    public Text nameText;
    public Text dialogueText;


    public DialogueTrigger triggerDialogue;

    private Queue<string> sentences;

    bool isplayerInside = false;
    bool dialogueIsStarted = false;

    #endregion

    #region START
    void Start()
    {
        sentences = new Queue<string>();
    }
    #endregion

    #region UPDATE
    private void Update()
    {
        if (dialogueIsStarted && Input.anyKeyDown)
        {
            DisplayNextSentence();
        }

        if (isplayerInside && Input.GetKeyDown(KeyCode.E) && !dialogueIsStarted)
        {
            dialogueIsStarted = true;
            triggerDialogue.TriggerDialogue();
        }
        
    }
    #endregion


    #region START DIALOGUE
    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.nameNPC;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    #endregion

    #region DISPLAY NEXT SENTENCE
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
    #endregion

    #region TYPE SENTENCE
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;

        }

    }
    #endregion

    #region END DIALOGUE
    public void EndDialogue()
    {
        Debug.Log("End of Conversation");
        dialogueIsStarted = false;
    }
    #endregion

    #region TRIGGER ENTER
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isplayerInside = true;
        }
    }
    #endregion

    #region TRIGGER EXIT
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isplayerInside = false;
        }
    }
    #endregion

}
