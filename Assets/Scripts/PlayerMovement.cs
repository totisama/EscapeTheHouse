using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0.0f)
        {
            transform.position += new Vector3(horizontal * moveSpeed * Time.deltaTime, 0, 0);
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
