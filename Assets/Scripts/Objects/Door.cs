using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private DoorSide side;
    [SerializeField] private DoorState state;
    [SerializeField] private int frontSortOrder;
    [Header("Closed state")]
    [SerializeField] private Color closedBackgroundColor;
    [SerializeField] private Globals.ItemTypes itemTypeToOpen;
    [SerializeField] private GameObject handle;
    [Header("Opened state")]
    [SerializeField] private Color openedBackgroundColor;
    [Header("Sides")]
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private SpriteRenderer front;
    [SerializeField] private SpriteRenderer back;

    private Collider2D coll;

    enum DoorSide
    {
        right,
        left
    }

    enum DoorState
    {
        opened,
        closed
    }

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void Start()
    {
        SetDoorSidesSortingOrder();

        if (state == DoorState.opened)
        {
            DoorOpened();
        }
        else
        {
            DoorClosed();
        }
    }

    private void SetDoorSidesSortingOrder()
    {
        if (side == DoorSide.left)
        {
            front.sortingOrder = frontSortOrder;
            back.sortingOrder = 0;
        }
        else
        {
            front.sortingOrder = 0;
            back.sortingOrder = frontSortOrder;
        }
    }

    private void DoorOpened()
    {
        background.color = openedBackgroundColor;
        handle.SetActive(false);
        coll.isTrigger = true;
    }

    private void DoorClosed()
    {
        background.color = closedBackgroundColor;
        handle.SetActive(true);
        coll.isTrigger = false;

        if (side == DoorSide.left)
        {
            back.sortingOrder = frontSortOrder;        
        }
        else
        {
            front.sortingOrder = frontSortOrder;        
        }
    }

    public void OpenDoor()
    {
        background.color = openedBackgroundColor;
        handle.SetActive(false);
        coll.isTrigger = true;
        state = DoorState.opened;

        SetDoorSidesSortingOrder();
    }

    public Globals.ItemTypes GetItemTypeToOpen()
    {
        return itemTypeToOpen;
    }
}
