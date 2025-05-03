using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class NoteFall : MonoBehaviour
{
    private float speed = 10f;

    Vector3 velocity = Vector3.zero;


    [Header("Target Settings")]
    [SerializeField] private Vector3 targetPosition = new Vector3(0,-9,0); // Set in inspector or via code
    [SerializeField] private float targetThreshold = 0.1f; // Distance to trigger completion

    [Header("Movement Settings")]
    [SerializeField] private float baseFallSpeed = 250f;
    [SerializeField] private float flutterSpeed = 7f;
    [SerializeField] private float flutterAmount = 0.7f;
    [SerializeField] private float fallAcceleration = 20f;

    [Header("Rotation Settings")]
    [SerializeField] private float maxRotationAngle = 75f; // Max degrees to rotate in each direction
    [SerializeField] private float rotationSpeed = 0.1f; // Degrees per second
    [SerializeField] private float rotationChangeFrequency = 0f; // How often rotation direction changes

    private float currentFallSpeed;
    private float flutterOffset;
    private float currentRotation;
    private float rotationDirection = 1f;
    private float nextRotationChangeTime;
    private bool hasReachedTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasReachedTarget) return;

        // Update falling speed
        currentFallSpeed += fallAcceleration * Time.deltaTime;

        // Horizontal flutter movement
        float horizontalMovement = Mathf.Sin(Time.time * flutterSpeed + flutterOffset) * flutterAmount;

        // Apply movement
        transform.position += new Vector3(
            horizontalMovement * Time.deltaTime,
            -currentFallSpeed * Time.deltaTime,
            0
        );

        // Handle Z-axis rotation
        UpdateRotation();

        // Check for target reach
        if (transform.position.y <= targetPosition.y + targetThreshold)
        {
            hasReachedTarget = true;
            Debug.Log("done");
            transform.position = new Vector3(
                transform.position.x,
                targetPosition.y,
                transform.position.z
            );
        }
    }

    void UpdateRotation()
    {
        // Change rotation direction periodically
        if (Time.time >= nextRotationChangeTime)
        {
            rotationDirection *= -1f;
            nextRotationChangeTime = Time.time + (1f / rotationChangeFrequency);
        }

        // Rotate around Z-axis
        currentRotation += rotationDirection * rotationSpeed * Time.deltaTime;
        currentRotation = Mathf.Clamp(currentRotation, -maxRotationAngle, maxRotationAngle);
        transform.localEulerAngles = new Vector3(0, 0, currentRotation);
    }


    private void OnBecameInvisible()
    {
        //this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {

        currentFallSpeed = baseFallSpeed;
        flutterOffset = Random.Range(0f, 100f);
        nextRotationChangeTime = Time.time + (1f / rotationChangeFrequency);

        hasReachedTarget = true;
        StartCoroutine(FallDownCountDown());
    }

    private void OnDisable()
    {
        GetComponent<NoteFall>().enabled = false;
    }
    public IEnumerator NoteRemoveCD()
    {

        yield return new WaitForSeconds(1.5f);
        LearningSystem.instance.stickyNotesCount = LearningSystem.instance.stickyNotesCount - 1 ;
        LearningSystem.instance.noteCountText.text = LearningSystem.instance.stickyNotesCount.ToString();
    }


    public IEnumerator FallDownCountDown()
    {
        yield return new WaitForSeconds(LevelManager.level.noteTimer);
        //StartCoroutine(LerpToTarget());
        StartCoroutine(NoteRemoveCD());
        hasReachedTarget = false;
    }
    public IEnumerator LerpToTarget()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPoint = new Vector3(transform.position.x, -12f, transform.position.z); ;
        float distance = Vector3.Distance(startPosition, endPoint);
        float duration = distance / speed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate interpolation factor (0 to 1)
            float t = elapsedTime / duration;

            // Lerp the position
            transform.position = Vector3.MoveTowards(startPosition, endPoint, t);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            yield return null; // Wait until next frame
        }

        // Ensure we exactly reach the target position
        transform.position = endPoint;
        this.gameObject.SetActive(false);
        //StartCoroutine(LerpToTarget());

    }
}
