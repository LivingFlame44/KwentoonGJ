using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Girl : MonoBehaviour
{
    public GameObject girl;
    public float speed;
    public Animator animator;

    public bool firstTaken;
    public TextMeshPro chatText;
    public string[] sentenceList;
    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        speed = 3.5f;
    }

    private void Awake()
    {
        chatText = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToNewSpot()
    {
        
    }


    public IEnumerator MoveToPlace(Vector3 endPoint, float duration)
    {
        Vector3 startPoint = transform.position;
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            transform.position = Vector2.Lerp(startPoint, endPoint, t / duration);
            yield return 0;
        }
        Debug.Log("Point Reached");
    }

    public IEnumerator LerpToTarget(Vector3 endPoint, GameObject stickyNote)
    {
        endPoint = endPoint + new Vector3(0, 0.65f, 0);
        Vector3 startPosition = transform.position;

        if(endPoint.x > startPosition.x)
        {
            animator.SetBool("isRight", true);
        }
        else
        {
            animator.SetBool("isRight", false);
        }

        float distance = Vector3.Distance(startPosition, endPoint);
        float duration = distance / speed;
        float elapsedTime = 0f;

        animator.SetBool("isMoving", true);
        while (elapsedTime < duration)
        {
            // Calculate interpolation factor (0 to 1)
            float t = elapsedTime / duration;

            // Lerp the position
            transform.position = Vector3.Lerp(startPosition, endPoint, t);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            yield return null; // Wait until next frame
        }

        // Ensure we exactly reach the target position
        transform.position = endPoint;
        animator.SetBool("isMoving", false);
        if(firstTaken)
        {
            ChangeChat();
        }
        else
        {
            firstTaken = true;
        }
        stickyNote.SetActive(true);

    }

    public void ChangeChat()
    {
        int randomNum = Random.Range(0, sentenceList.Length);
        chatText.text = sentenceList[randomNum];
    } 
}
