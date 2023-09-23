using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private bool onDoor;
    private bool onSafe;
    private bool onBookstore;
    private Door door;
    private Safe safe;
    private Bookstore bookstore;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InventoryManager.Instance.ActivateItem();
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            PressEKey();
        }
    }

    private void PressEKey()
    {
        if (onDoor)
        {
            UseItemOnDoor();
        }
        if (onBookstore)
        {
            UseItemOnBookstore();
        }
        else if (onSafe)
        {
            safe.TogglePad();
        }
    }

    private void UseItemOnDoor()
    {
        if (!AbleToOpenDoor())
        {
            return;
        }

        door.OpenDoor();
        InventoryManager.Instance.UseItem();
    }
    
    private void UseItemOnBookstore()
    {
        if (!AbleToPlaceItem())
        {
            return;
        }

        bookstore.PlaceBook();
        InventoryManager.Instance.UseItem();
    }

    private bool AbleToOpenDoor()
    {
        return InventoryManager.Instance.GetCurrentUsableType() == door.GetItemTypeToOpen();
    }
    
    private bool AbleToPlaceItem()
    {
        return InventoryManager.Instance.GetCurrentUsableType() == bookstore.GetItemTypeToPlace();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            onDoor = true;
            door = collision.gameObject.GetComponent<Door>();
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            onDoor = false;
            door = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Safe"))
        {
            onSafe = true;
            safe = collision.gameObject.GetComponent<Safe>();
        }
        else if (collision.gameObject.CompareTag("Bookstore"))
        {
            onBookstore = true;
            bookstore = collision.gameObject.GetComponent<Bookstore>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Safe"))
        {
            onSafe = false;
            safe = null;
        }
        else if (collision.gameObject.CompareTag("Door"))
        {
            onDoor = false;
            door = null;
        }
        else if (collision.gameObject.CompareTag("Bookstore"))
        {
            onBookstore = false;
            bookstore = null;
        }
    }
}
