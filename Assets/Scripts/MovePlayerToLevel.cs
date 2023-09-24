using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayerToLevel : MonoBehaviour
{
    [SerializeField] private float goYPosition;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private RoomBounds nextRoom;
    [SerializeField] private GameObject hideScreenObject;

    private bool inBounds;

    private void Start()
    {
        hideScreenObject.SetActive(false);
    }

    private void Update()
    {
        if (inBounds && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            StartCoroutine(ChangeLevel());
        }
    }

    private void MoveCamera()
    {
        nextRoom.UpdateRoomBounds();
        GameManager.Instance.ChangeRoom();
    }

    public void MovePlayerToNextLevel()
    {
        playerTransform.position = new Vector3(playerTransform.position.x, goYPosition, 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inBounds = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inBounds = false;
        }
    }

    IEnumerator ChangeLevel()
    {
        AudioManager.Instance.PlaySFXSound("Stairs");
        hideScreenObject.SetActive(true);
        GameManager.Instance.TogglePlayer(false);
        yield return new WaitForSeconds(1.5f);
        MovePlayerToNextLevel();
        MoveCamera();
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.TogglePlayer(true);
        hideScreenObject.SetActive(false);
    }
}
