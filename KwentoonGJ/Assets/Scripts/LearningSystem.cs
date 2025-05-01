using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Searcher;
using UnityEngine;

public class LearningSystem : MonoBehaviour
{
    public static LearningSystem instance;
    public RandomSpawn randomSpawn;

    public bool hasStartedCD;
    public int stickyNotesCount;

    public GameObject girl;
    public Girl girlScript; 

    public Word currentWord;
    public GameObject currentNote;
    public int currentIndex;

    public GameObject learningPanel;
    public TextMeshProUGUI wordText, correctText, meaningText;
    public TMP_InputField inputField;

    public GameObject notesParent;
    public GameObject stickyNotePrefab;
    public List<GameObject> activeNotesList = new List<GameObject>();
    public List<GameObject> inactiveNotesList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        girlScript = girl.GetComponent<Girl>();

        currentWord = LevelManager.instance.wordList[0];
        StartNewLesson();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasStartedCD)
        {
            if(stickyNotesCount == 0)
            {
                //GAmeover
            }        
        }
    }

    public void ShowLearningPanel()
    {
        learningPanel.SetActive(true);

        wordText.text = currentWord.word;
        meaningText.text = currentWord.wordInfo;

    }

    public void SpawnNewNote(Vector3 notePos)
    {
        
        if (inactiveNotesList.Count == 0)
        {
            currentNote = Instantiate(stickyNotePrefab, notesParent.transform);
            activeNotesList.Add(currentNote);
        }
        else
        {
            currentNote = inactiveNotesList[0];
            activeNotesList.Add(currentNote);
            inactiveNotesList.RemoveAt(0);

            currentNote.GetComponent<StickyNote>().enabled = true;


        }
        currentNote.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = currentWord.word;
        currentNote.transform.GetChild(0).gameObject.SetActive(false);
        currentNote.transform.position = notePos;
        currentNote.SetActive(false);
    }

    public void StartNewLesson()
    {
        Vector3 notePos = randomSpawn.GetRandomPosition();

        SpawnNewNote(notePos);

        var coroutine = girlScript.LerpToTarget(notePos, currentNote); 
        StartCoroutine(coroutine);
    }
    
    public void NextWord()
    {
        currentIndex++;
        currentWord = LevelManager.instance.wordList[currentIndex];
    }

    public void Type()
    {
        CheckCorrectLetter();
        if (inputField.text.ToLower() == currentWord.word.ToLower())
        {
            OnAnswer();
        }
    }

    public void OnAnswer() 
    {
        if(currentIndex == LevelManager.instance.level.wordTotal - 1)
        {

            //start quiz
        }

        else
        {
            learningPanel.SetActive(false);

            stickyNotesCount = stickyNotesCount + 1;
            hasStartedCD = true;

            currentNote.transform.GetChild(0).gameObject.SetActive(true);
            currentNote.GetComponent<Collider2D>().enabled = false;
            NextWord();
            StartNewLesson();
            ResetLearningPanel();
        }

    }

    public void ResetLearningPanel()
    {
        inputField.text = "";
        correctText.text = "";
    }


    public void CheckCorrectLetter()
    {
        string correctLetter = "";

        string inputText = inputField.text.ToLower(); // Convert to uppercase for case-insensitive check
        int minLength = Mathf.Min(inputText.Length, currentWord.word.ToLower().Length);

        for (int i = 0; i < minLength; i++)
        {
            if (inputText[i] == currentWord.word.ToLower()[i])
            {
                correctLetter = correctLetter + currentWord.word[i];
            }
        }

        //foreach (char c in inputField.text.ToLower())
        //{
              
        //    if (currentWord.word.ToLower()[i] != null)
        //    {
        //        if (c == currentWord.word.ToLower()[i])
        //        {
        //            correctLetter = correctLetter + currentWord.word[i];

        //        }
        //    }      
        //    i++;
        //}
        correctText.text = correctLetter;
    }
}
