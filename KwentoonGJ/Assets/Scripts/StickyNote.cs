using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyNote : MonoBehaviour
{
    public Word word;
    public GameObject interactText;
    public bool hasInteracted;
    public bool canInteract;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && canInteract)
        {
            Debug.Log("Interact");
            LearningSystem.instance.ShowLearningPanel();
            interactText.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(LearningSystem.instance.isAnswering == false)
            {
                //Debug.Log("Collide");
                interactText.SetActive(true);
                canInteract = true;
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactText.SetActive(false);
            canInteract = false;
        }
    }


}
