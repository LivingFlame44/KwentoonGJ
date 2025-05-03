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

    }

    public void Menu()
    {
        Time.timeScale = 1f;
        //UIManager.currentMenuScene = UIManager.MenuPanels.MAINMENU;
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
        //UIManager.currentMenuScene = UIManager.MenuPanels.SETTINGS;
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Continue()
    {
        SceneManager.LoadScene("MainMenu");
        //UIManager.currentMenuScene = UIManager.MenuPanels.LEVELSELECT;
        Time.timeScale = 1f;
    }

    public void RollCredits()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("CreditsCutscene");
    }
}
