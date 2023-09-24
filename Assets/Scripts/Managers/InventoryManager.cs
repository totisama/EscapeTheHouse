using System;
using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] private Transform machineAnchor;
    [SerializeField] private InventorySlot slot;
    [SerializeField] private string UILayerName;

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
            AudioManager.Instance.PlaySFXSound("Interact");
        }
        else if (usable == null)
        {
            usable = item;

            AddToUI(item);
            AudioManager.Instance.PlaySFXSound("Interact");
        }
    }

    private void AddToUI(Item item)
    {
        item.gameObject.transform.SetParent(slot.transform);
        item.InitialiceItemUI();
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
        item.gameObject.transform.position = Vector3.zero;
        item.gameObject.transform.SetParent(machineAnchor, false);
        item.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public Globals.ItemTypes GetCurrentUsableType()
    {
        if (usable == null)
        {
            return Globals.ItemTypes.none;
        }

        return usable.GetItemType();
    }
}
