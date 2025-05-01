using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Word", menuName = "ScriptableObjects/Word/Word")]
public class Word : ScriptableObject
{
    public string word;

    [TextArea(3, 10)]
    public string wordInfo;

    public string[] wordSpellingOptions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
