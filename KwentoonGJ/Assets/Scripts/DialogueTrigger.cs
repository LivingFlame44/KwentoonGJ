using System.Collections;
using System.Collections.Generic;
using TMPro;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}
[System.Serializable]
public class DialogueLine
{
    public Sprite bgImage;
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;

    public UnityEvent onEndLineEvent;
}
[System.Serializable]
public class Dialogue
{
    //public string dialogueName;
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

[CreateAssetMenu(fileName = "DialogueTrigger", menuName = "ScriptableObjects/DialogueTrigger")]
public class DialogueTrigger : ScriptableObject
{
    [SerializeField]
    public Dialogue dialogue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TriggerDialogue()
    {
        Debug.Log("talk");
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    public virtual void Choose()
    {

    }

}
