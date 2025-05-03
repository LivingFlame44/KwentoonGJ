using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class Level : ScriptableObject
{
    public int levelNum;
    public int wordTotal;
    public int quizQuestionsTotal;
    
    public Word[] wordList;

    public float noteTimer;
    public DialogueTrigger introDialogue;
    public DialogueTrigger outroDialogue;
    // Start is called before the first frame update
    public DialogueType dialogueType;
    public enum DialogueType
    {
        Intro,
        End,
        None
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
