using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InventoryManager.Instance.UseItem(Globals.ItemTypes.machine);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            InventoryManager.Instance.UseItem(Globals.ItemTypes.normal);
        }
    }
}
