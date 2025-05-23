using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;


    public Animator animator;

    private Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(LearningSystem.instance.isAnswering == false)
        {
            ProcessInputs();
        }

        
    }

    void FixedUpdate()
    {
        if (LearningSystem.instance.isAnswering == false)
        {
            Move();
        }
        else
        {
            animator.SetFloat("Horizontal", 0);
            //animator.SetFloat("Vertical", moveY);
            animator.SetFloat("Speed", 0);
            moveDirection = Vector2.zero;
            rb.velocity = Vector2.zero;
        }
        
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        animator.SetFloat("Horizontal", moveX);
        //animator.SetFloat("Vertical", moveY);
        animator.SetFloat("Speed", new Vector2(moveX, moveY).sqrMagnitude);
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
