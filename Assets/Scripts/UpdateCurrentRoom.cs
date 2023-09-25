using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCurrentRoom : MonoBehaviour
{
    [SerializeField] private RoomBounds leftRoom;
    [SerializeField] private RoomBounds rightRoom;
    [SerializeField] private Transform playerTransform;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool comesFromRight;

            if (playerTransform.position.x < transform.position.x)
            {
                leftRoom.UpdateRoomBounds();
                comesFromRight = true;
            }
            else
            {
                rightRoom.UpdateRoomBounds();
                comesFromRight = false;
            }

            GameManager.Instance.ChangeRoom(comesFromRight);
        }
    }
}
