using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBounds : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private Vector2 ghostLeftPosition;
    [SerializeField] private Vector2 ghostRightPosition;

    private Bounds bounds;

    private void Start()
    {
        bounds = GetComponent<Collider2D>().bounds;
    }

    public void UpdateRoomBounds()
    {
        Globals.RoomBounds = GetComponent<Collider2D>().bounds;
    }

    public Bounds GetRoomBounds()
    {
        return bounds;
    }

    public Vector2 GetGhostPosition(bool leftPosition)
    {
        return leftPosition ? ghostLeftPosition : ghostRightPosition;
    }
}
