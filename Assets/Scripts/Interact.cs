using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private bool onDoor;
    private bool onSafe;
    private Door door;
    private Safe safe;

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
            UseItemFunction();
        }
        else if (onSafe)
        {
            safe.OpenPad();
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
        else if (collision.gameObject.CompareTag("Safe"))
        {
            onSafe = true;
            safe = collision.gameObject.GetComponent<Safe>();
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            onDoor = false;
            door = null;
        }
        else if (collision.gameObject.CompareTag("Safe"))
        {
            onSafe = false;
            safe = null;
        }
    }
}
