using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;
    public Image charIcon;
    public Image sceneBG;
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
        Time.timeScale = 1.0f;
        lines = new Queue<DialogueLine>();
        //cam = Camera.main;
        if(LevelManager.level.dialogueType == Level.DialogueType.Intro)
        {
            LevelManager.level.introDialogue.TriggerDialogue();
        }
        else
        {
            LevelManager.level.outroDialogue.TriggerDialogue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckNextDialogueClick();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (LevelManager.level.dialogueType == Level.DialogueType.Intro)
            {
                SceneManager.LoadScene("GameScene");
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
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

    public void StartDialogue(Dialogue dialogue)
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

        if (currentLine.bgImage == null)
        {

        }
        else
        {
            sceneBG.gameObject.SetActive(true);
            sceneBG.sprite = currentLine.bgImage;
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

        if (LevelManager.level.dialogueType == Level.DialogueType.Intro)
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
        
    }

 
}
