using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    private bool onDoor;
    private Door door;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InventoryManager.Instance.ActivateItem();
        }
        else if (Input.GetKeyUp(KeyCode.E) && onDoor)
        {
            UseItemFunction();
        }
    }

    private void UseItemFunction()
    {
        if (!AvailableToUse())
        {
            return;
        }

        door.OpenDoor();
        InventoryManager.Instance.UseItem();
    }

    private bool AvailableToUse()
    {
        if (InventoryManager.Instance.GetCurrentUsableType() == door.GetItemTypeToOpen())
        {
            return true;
        }

        return false;
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
}
