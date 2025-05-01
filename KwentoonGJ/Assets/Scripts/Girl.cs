using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Girl : MonoBehaviour
{
    public GameObject girl;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3.5f;
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
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, endPoint);
        float duration = distance / speed;
        float elapsedTime = 0f;

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

        stickyNote.SetActive(true);

    }
}
