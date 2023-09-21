using System;
using System.Collections;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] private Transform machineAnchor;

    private Item activatable;
    private Item usable;

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
        if (item.GetItemType() == Globals.ItemTypes.machine)
        {
            activatable = item;
            AnchorToPlayer(item);
        }
        else if (usable == null)
        {
            usable = item;
        }
    }

    public void ActivateItem()
    {
        if (activatable == null)
        {
            return;
        }

        GameManager.Instance.ChangeNight();
    }

    public void UseItem()
    {
        if (usable == null)
        {
            return;
        }

        Destroy(usable.gameObject);
        usable = null;
    }

    private void AnchorToPlayer(Item item)
    {
        item.transform.position = Vector3.zero;
        item.transform.SetParent(machineAnchor, false);
    }

    public Globals.ItemTypes GetCurrentUsableType()
    {
        return usable.GetItemType();
    }
}
