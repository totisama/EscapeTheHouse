using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookshelf : MonoBehaviour
{
    [SerializeField] private Globals.ItemTypes itemTypeToPlace;
    [SerializeField] private Door doorToOpen;
    [SerializeField] private Sprite fullBookshelf;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void PlaceBook()
    {
        doorToOpen.OpenDoor();
        sr.sprite = fullBookshelf;
    }

    public Globals.ItemTypes GetItemTypeToPlace()
    {
        return itemTypeToPlace;
    }
}
