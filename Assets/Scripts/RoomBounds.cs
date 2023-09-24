using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBounds : MonoBehaviour
{
    public void UpdateRoomBounds()
    {
        Globals.RoomBounds = GetComponent<Collider2D>().bounds;
    }
}
