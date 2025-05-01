using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Level level;

    //public int wordTotal;
    //public int quizQuestionsTotal;


    public List<Word> wordList = new List<Word>();
    public List<Word> wordsInQuiz = new List<Word>();
    // Start is called before the first frame update
    void Start()
    {
        

    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        AssignQuizQuestions();
        AssignWordList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignQuizQuestions() 
    {
        for (int i = 0; i < level.quizQuestionsTotal;)
        {
            int randomNum = Random.Range(0, level.wordList.Length);
            if (wordsInQuiz.Contains(level.wordList[randomNum]))
            {
                continue;
            }
            else
            {
                wordsInQuiz.Add(level.wordList[randomNum]);
                i++;
            }
            
        }
    }



    public void AssignWordList()
    {
        //adds words from quiz
        foreach (Word word in wordsInQuiz)
        {
            wordList.Add(word);
        }

        //fills in rest of empty
        for(int i = level.quizQuestionsTotal; i < level.wordTotal; i++)
        {
            int randomNum = Random.Range(0, level.wordList.Length);
            wordList.Add(level.wordList[randomNum]);
            Debug.Log("added" + level.wordList[randomNum]);
        }

        wordList.Shuffle();
    }
       

}
