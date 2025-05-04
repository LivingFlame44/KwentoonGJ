using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class QuizManager : MonoBehaviour
{
    public int currentQuestionNumIndex;
    public GameObject[] choiceBtns;
    public TextMeshProUGUI questionMeaningText;


    public GameObject quizPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignChoices()
    {
        questionMeaningText.text = LevelManager.instance.wordsInQuiz[currentQuestionNumIndex].wordInfo;

        LevelManager.instance.wordsInQuiz[currentQuestionNumIndex].wordSpellingOptions.Shuffle();

        for(int i = 0; i < choiceBtns.Length; i++)
        {
            choiceBtns[i].GetComponentInChildren<TextMeshProUGUI>().text = LevelManager.instance.wordsInQuiz[currentQuestionNumIndex].wordSpellingOptions[i];

            if(LevelManager.instance.wordsInQuiz[currentQuestionNumIndex].wordSpellingOptions[i] == LevelManager.instance.wordsInQuiz[currentQuestionNumIndex].word)
            {
                choiceBtns[i].GetComponent<Button>().onClick.RemoveAllListeners();
                choiceBtns[i].GetComponent<Button>().onClick.AddListener(CorrectAnswer);
            }

            else
            {
                choiceBtns[i].GetComponent<Button>().onClick.RemoveAllListeners();
                choiceBtns[i].GetComponent<Button>().onClick.AddListener(WrongAnswer);
            }

            choiceBtns[i].GetComponent<Button>().onClick.AddListener(LearningSystem.instance.PlayBtnSFX2);
        }
    }

    public void CorrectAnswer()
    {
        if(currentQuestionNumIndex == LevelManager.level.quizQuestionsTotal - 1)
        {
            //level clear
            GameManager.instance.LevelClear();
            Debug.Log("stage CLear");
        }
        else
        {
            currentQuestionNumIndex++;
            AssignChoices();
        }
    }

    public void WrongAnswer()
    {
        //gameover

        GameManager.instance.QuizGameOver();
    }
}
