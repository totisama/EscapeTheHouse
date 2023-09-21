using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Color backgroundColor;
    [SerializeField] private DoorSide side;
    [Header("Sides")]
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private SpriteRenderer front;
    [SerializeField] private SpriteRenderer back;

    enum DoorSide
    {
        right,
        left
    }

    private void Awake()
    {
        background.color = backgroundColor;

        if (side == DoorSide.left)
        {
            front.sortingOrder = 6;
            back.sortingOrder = 0;
        }
        else
        {
            front.sortingOrder = 0;
            back.sortingOrder = 6;
        }
    }
}
