using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CameraController cam;
    [SerializeField] RoomBounds startingRoom;

    public static GameManager Instance;

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

    private void Start()
    {
        startingRoom.UpdateRoomBounds();
    }

    public void ChangeRoom()
    {
        cam.UpdateCameraBounds();
    }
}
