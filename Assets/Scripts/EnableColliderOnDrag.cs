using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableColliderOnDrag : MonoBehaviour
{
    [SerializeField] Rigidbody2D rbToEnable;
    [SerializeField] float minDistanceToEnable;

    private float initialPosition;
    private float minXPosition;
    private float maxXPosition;
    public bool RbEnabled { get; private set; }

    private void Start()
    {
        initialPosition = transform.position.x;
        minXPosition = initialPosition - minDistanceToEnable;
        maxXPosition = initialPosition + minDistanceToEnable;

        rbToEnable.simulated = false;
    }

    private void Update()
    {
        float xPosition = transform.position.x;

        if (!RbEnabled && (xPosition < minXPosition || xPosition > maxXPosition))
        {
            EnableObject();
        }
    }

    private void EnableObject()
    {
        rbToEnable.simulated = !rbToEnable.simulated;
        RbEnabled = true;
    }
}
