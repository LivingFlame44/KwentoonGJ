using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject menuPanel;
    public GameObject levelSelectPanel;
    public GameObject difficultyPanel;

    public Level[] levels;

    public static bool introWatched;
    public enum MenuPanels
    {
        MAINMENU,
        LEVELSELECT,
        SETTINGS
    }

    public static MenuPanels currentMenuScene = MenuPanels.MAINMENU;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelSelectMenu()
    {
        levelSelectPanel.SetActive(true);
        //if (!introWatched)
        //{
        //    SceneManager.LoadScene("IntroCutscene");
        //}
        //else
        //{
        //    levelSelectPanel.SetActive(true);
        //}
    }

    public void ShowMainMenu()
    {
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
    }

    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
        menuPanel.SetActive(false);
    }


    public void Quitgame()
    {
        Application.Quit();
    }

    public void SettingsBack()
    {
        if (SceneManager.loadedSceneCount > 1)
        {
            SceneManager.UnloadSceneAsync("MainMenu");
        }
        else
        {
            ShowMainMenu();
        }

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
