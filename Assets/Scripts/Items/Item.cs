using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Globals.ItemTypes type;

    public Globals.ItemTypes GetItemType() { return type; }
}
