using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject difficultyPanel;

    public Level[] levels;
    public int[] lsevels;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLevel(int i)
    {
        LevelManager.level = levels[i];
        //SceneManager.LoadScene("GameScene");
        difficultyPanel.SetActive(true);
        LevelManager.level.dialogueType = Level.DialogueType.Intro;
    }


    public void SelectEasyDifficulty()
    {
        Debug.Log("SelectHardDifficulty called!");
        LevelManager.level.noteTimer = 15;
        if (LevelManager.level.introDialogue == null)
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            SceneManager.LoadScene("DialogueScene");
        }
    }

    public void SelectHardDifficulty()
    {
        Debug.Log("SelectHardDifficulty called!");
        LevelManager.level.noteTimer = 9;
        if (LevelManager.level.introDialogue == null)
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            SceneManager.LoadScene("DialogueScene");
        }
    }
}
