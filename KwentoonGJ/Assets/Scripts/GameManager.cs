using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public delegate void OnGameOver();
    public OnGameOver gameOver;

    public GameObject gameOverPanel;
    public GameObject stageClearPanel, quizPanel;
    public GameObject quizFailText, timeRunOutText;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        //gameOver += GameOver;
    }


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuizGameOver()
    {
        gameOverPanel.SetActive(true);
        quizFailText.SetActive(true);
        AudioManager.Instance.PlayMusic("Lose Music");
        Time.timeScale = 0f;
    }

    public void TimeGameOver()
    {
        gameOverPanel.SetActive(true);
        timeRunOutText.SetActive(true);
        AudioManager.Instance.PlayMusic("Lose Music");
        Time.timeScale = 0f;
    }
    public void LevelClear()
    {
        stageClearPanel.SetActive(true);
        AudioManager.Instance.PlayMusic("Win Music");
        Time.timeScale = 0f;
    }
}
