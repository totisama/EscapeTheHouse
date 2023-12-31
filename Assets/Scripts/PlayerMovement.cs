using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    private Rigidbody2D rb;
    private Animator anim;
    private float horizontal;
    private bool canMove;
    public bool CanMove {
        get { return canMove; } 
        set { 
            if (!value)
            {
                rb.velocity = Vector3.zero;
            }

            canMove = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        CanMove = true; 
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        if (!CanMove)
        {
            anim.SetBool("move", false);
            return;
        }

        if (horizontal == 0.0f)
        {
            anim.SetBool("move", false);
        }
        else
        {
            anim.SetBool("move", true);
        }

        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (horizontal != 0.0f)
        {
            FlipScale(horizontal);
        }
    }

    private void FlipScale(float horizontal)
    {
        Vector3 scale = transform.localScale;

        if (horizontal < 0)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }
}
