using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    [SerializeField] private Color highlightColor;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float distanceToHighligh;

    private SpriteRenderer sr;
    private Color initialColor;
    
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();    
    }

    private void Start()
    {
        initialColor = sr.color;
    }

    private void Update()
    {
        float playerXPosition = playerTransform.position.x;
        float minDistance = transform.position.x - distanceToHighligh;
        float maxDistance = transform.position.x + distanceToHighligh;

        if (playerXPosition > minDistance && playerXPosition < maxDistance)
        {
            HighlightObject(true);
        }
        else
        {
            HighlightObject(false);
        }
    }

    private void HighlightObject(bool highlight)
    {
       sr.color = highlight ? highlightColor : initialColor;
    }
}
