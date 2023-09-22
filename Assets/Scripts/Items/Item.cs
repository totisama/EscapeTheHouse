using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private Globals.ItemTypes type;
    [SerializeField] private Sprite sprite;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void InitialiceItemUI()
    {
        image.sprite = sprite;
    }

    public Globals.ItemTypes GetItemType() { return type; }
}
