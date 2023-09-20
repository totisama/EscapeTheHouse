using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private bool onItem;
    private Item currentItem;

    private void Update()
    {
        if (onItem && Input.GetKeyDown(KeyCode.Q))
        {
            InventoryManager.Instance.AddItem(currentItem);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CollectableItem"))
        {
            onItem = true;
            currentItem = collision.gameObject.GetComponent<Item>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CollectableItem"))
        {
            onItem = false;
            currentItem = null;
        }
    }
}
