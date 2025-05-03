using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;
    public Image charIcon;
    public TextMeshProUGUI charName;
    public TextMeshProUGUI charDialogue;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;
    public float typingSpeed;
    public string currentText;

    public DialogueLine currentLine;

    public TMP_FontAsset nameFont;
    public TMP_FontAsset messageFont;

    //public LayerMask canNextDialogue;
    //Camera cam;
    //public float lineWidth;

    //private bool castRays = true;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
    }
    void Start()
    {
        lines = new Queue<DialogueLine>();
        //cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //if(castRays)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        //    {
        //        if(hit.transform.gameObject.layer == canNextDialogue)
        //        {

        //        }
        //    }
        //}
        //Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        //RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, canNextDialogue);

    }

    public void CheckNextDialogueClick()
    {
        if (isDialogueActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (charDialogue.text == currentLine.line)
                {
                    currentLine.onEndLineEvent.Invoke();
                    DisplayNextDialogueLine();

                }

                else
                {
                    StopAllCoroutines();
                    charDialogue.text = currentLine.line;

                }
            }
        }
    }

    public void StartDialogue(Dialogue1 dialogue)
    {
        if (dialogue.dialogueLines.Count != 0)
        {
            dialoguePanel.SetActive(true);
            isDialogueActive = true;

            lines.Clear();

            foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
            {
                lines.Enqueue(dialogueLine);
            }

            DisplayNextDialogueLine();
        }
        else
        {
            EndDialogue();
        }
    }

    public void DisplayNextDialogueLine()
    {

        //checks if end of dialogue ands has no choices
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        //currentText = lines.Peek().line;
        if (lines.Count == 0)
        {
            EndDialogue();
        }
        else
        {
            currentLine = lines.Dequeue();
        }

        //assigns character name
        if (currentLine.character.name == null)
        {
            charName.gameObject.SetActive(false);
        }
        else
        {

            charName.gameObject.SetActive(true);
            charName.text = currentLine.character.name;
        }

        if (currentLine.character.icon == null)
        {
            charIcon.gameObject.SetActive(false);
        }
        else
        {

            charIcon.gameObject.SetActive(true);
            charIcon.sprite = currentLine.character.icon;
        }


        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));


    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        charDialogue.text = "";

        foreach (char c in dialogueLine.line.ToCharArray())
        {

            charDialogue.text += c;

            yield return new WaitForSeconds(typingSpeed);
        }

    }
    void EndDialogue()
    {
        isDialogueActive = false;
        StopAllCoroutines();

        dialoguePanel.gameObject.SetActive(false);
    }

 
}
