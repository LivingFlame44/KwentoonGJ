using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseBtn;

    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        MenuManager.currentMenuScene = MenuManager.MenuPanels.MAINMENU;
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;

    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;

    }
    public void Settings()
    {
        MenuManager.currentMenuScene = MenuManager.MenuPanels.SETTINGS;
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Continue()
    {
        if (LevelManager.level.outroDialogue == null)
        {
            MenuManager.currentMenuScene = MenuManager.MenuPanels.LEVELSELECT;
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1f;
        }
        else
        {
            LevelManager.level.dialogueType = Level.DialogueType.End;
            SceneManager.LoadScene("DialogueScene");
        }
    }

    public void RollCredits()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("CreditsCutscene");
    }
}
