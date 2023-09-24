using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nocturnal : MonoBehaviour
{
    [SerializeField] private Sprite daySprite;
    [SerializeField] private Sprite nightSprite;
    [SerializeField] private SpriteRenderer sr;
    [Header("Behavior")]
    [SerializeField] private bool changeSprite;
    [SerializeField] private bool changeActive;

    public void UpdateNocturnal(bool isNight)
    {
        if (changeActive)
        {
            gameObject.SetActive(isNight);
        }

        if (changeSprite)
        {
            ChangeSprite(isNight);
        }
    }

    private void ChangeSprite(bool isNight)
    {
        sr.sprite = isNight ? nightSprite : daySprite;
    }
}
