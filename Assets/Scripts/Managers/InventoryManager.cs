using System;
using System.Collections;
using UnityEditorInternal.Profiling.Memory.Experimental;
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
        }
        else if (usable == null)
        {
            usable = item;

            AddToUI(item);
        }
    }

    private void AddToUI(Item item)
    {
        SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
        sr.sortingLayerName = UILayerName;
        sr.sortingOrder = 2;
        item.gameObject.transform.SetParent(slot.transform);
        item.gameObject.transform.position = slot.transform.position;
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
