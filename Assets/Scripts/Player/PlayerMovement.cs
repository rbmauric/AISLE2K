using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement Values
    public float speed = 5;
    public float jumpVelocity;
    public float moveTime = 0.2f;

    //Controllers
    public bool right;
    public bool groundCheck;
    public bool canFlip;
    public bool canMove;
    private bool moving = false;

    //Misc
    private Animator animator;
    //public SoundManager sm;
    //public float soundLoop = 0.3f;
    //private bool soundActive;

    private void Start()
    {
        canMove = true;
        right = true;
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            playerMovement();
            Jump();
            Flip();
        }
    }

    void playerMovement()
    {
        if (Input.GetButton("Horizontal") && canMove)
        {
            moving = true;
            animator.SetFloat("Speed", speed);
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f);
            transform.position += movement * Time.deltaTime * speed;
        }
        else
        {
            moving = false;
            animator.SetFloat("Speed", 0);
        }
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0 && !right || Input.GetAxis("Horizontal") < 0 && right)
        {
            right = !right;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && groundCheck == true)
        {
            groundCheck = false;
            animator.SetBool("Jump", true);
            GetComponentInParent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
        }
    }
}