using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region VARIABLES
    public GameObject canvasDialogue;
    public Animator dialogueAnimator;

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

        canvasDialogue.SetActive(false);
    }
    #endregion

    #region UPDATE
    private void Update()
    {
        if (dialogueIsStarted && Input.anyKeyDown)
        {
            //Enseñamos la siguiente frase
            DisplayNextSentence();
        }

        if (isplayerInside && Input.GetKeyDown(KeyCode.E) && !dialogueIsStarted)
        {
            //activamos el canvas
            canvasDialogue.SetActive(true);

            //Activamos la animación
            dialogueAnimator.SetBool("isOpen", true);

            //Iniciamos el dialogo
            dialogueIsStarted = true;
            triggerDialogue.TriggerDialogue();
        }
        
    }
    #endregion


    #region START DIALOGUE
    public void StartDialogue(Dialogue dialogue)
    {
        //Cogemos el texto
        nameText.text = dialogue.nameNPC;

        //limpiamos la queue de frases
        sentences.Clear();

        //llenamos la queue de frases con el dialogo
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
        //Activamos la animación
        dialogueAnimator.SetBool("isOpen", false);

        Invoke("DeactivateCanvas", 0.45f);
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

    #region DEACTIVATE CANVAS
    void DeactivateCanvas()
    {
        //desactivamos el Canvas
        canvasDialogue.SetActive(false);
        dialogueIsStarted = false;
    }
    #endregion

}
