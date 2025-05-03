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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLevel(int i)
    {
        LevelManager.level = levels[i];
        SceneManager.LoadScene("GameScene");
    }
}
