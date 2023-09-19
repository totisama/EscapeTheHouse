using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0.0f)
        {
            transform.position += new Vector3(horizontal * moveSpeed * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = horizontal < 0.0f;
        }
    }
}
