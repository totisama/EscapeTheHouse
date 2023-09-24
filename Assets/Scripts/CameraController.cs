using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    private Camera cam;
    private Bounds cameraBounds;
    private Vector3 targetPosition;

    void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        UpdateCameraBounds();
    }

    private void LateUpdate()
    {
        targetPosition = playerTransform.position;

        targetPosition = GetCameraBounds();

        transform.position = targetPosition;
    }

    private Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(targetPosition.x, cameraBounds.min.x, cameraBounds.max.x),
            Mathf.Clamp(targetPosition.y, cameraBounds.min.y, cameraBounds.max.y),
            transform.position.z
        );
    }

    public void UpdateCameraBounds()
    {
        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        Bounds roomBounds = Globals.RoomBounds;

        float minX = roomBounds.min.x + width;
        float maxX = roomBounds.max.x - width;

        float minY = roomBounds.min.y + height;
        float maxY = roomBounds.max.y - height;

        cameraBounds = new Bounds();
        cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0.0f),
            new Vector3(maxX, maxY, 0.0f)
        );
    }
}
