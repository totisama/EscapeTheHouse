using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookstore : MonoBehaviour
{
    [SerializeField] private Globals.ItemTypes itemTypeToPlace;
    [SerializeField] private Door doorToOpen;
    [SerializeField] private Sprite fullBookstore;

    //private SpriteRenderer sr;

    private void Awake()
    {
        //sr = GetComponent<SpriteRenderer>();
    }

    public void PlaceBook()
    {
        doorToOpen.OpenDoor();
        //sr.sprite = fullBookstore;
    }

    public Globals.ItemTypes GetItemTypeToPlace()
    {
        return itemTypeToPlace;
    }
}
