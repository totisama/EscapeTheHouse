using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.StopBackground();
            AudioManager.Instance.PlaySFXSound("Complete");
            GameManager.Instance.FinishGame();
        }
    }
}
