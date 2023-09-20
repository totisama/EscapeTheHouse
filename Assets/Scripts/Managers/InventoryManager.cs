using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] private Transform machineAnchor;


    private List<Item> inventory = new List<Item>();

    public static InventoryManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(Item item)
    {
        if (inventory.Count < inventorySize)
        {
            Debug.Log("Item added: " + item.GetItemType());
            inventory.Add(item);

            if (item.GetItemType() == Globals.ItemTypes.machine)
            {
                AnchorToPlayer(item);
            }
        }
    }

    public void UseItem(Globals.ItemTypes type)
    {
        if (type == Globals.ItemTypes.normal)
        {
            UseNormalItem();
        }
        else if (type == Globals.ItemTypes.machine)
        {
            UseMachine();
        }
    }

    private void UseMachine()
    {
        Item item = inventory.Find(item => item.GetItemType() == Globals.ItemTypes.machine);

        if (!item)
        {
            return;
        }

        GameManager.Instance.ChangeNight();
    }

    private void UseNormalItem()
    {
        throw new NotImplementedException();
    }

    private void AnchorToPlayer(Item item)
    {
        item.transform.position = Vector3.zero;
        item.transform.SetParent(machineAnchor, false);
    }
}
