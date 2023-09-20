using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CameraController cam;
    [SerializeField] RoomBounds startingRoom;
    [Header("Night")]
    [Tooltip("In seconds")]
    [SerializeField] int nightDuration;
    [Tooltip("In seconds")]
    [SerializeField] int nightCooldown;

    private bool isNight;
    private float currentNightTime;
    private float timeToCharge;

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

    private void Update()
    {
        if (isNight && Time.time > currentNightTime)
        {
            ChangeNight(true);
        }
    }

    public void ChangeRoom()
    {
        cam.UpdateCameraBounds();
    }

    public void ChangeNight(bool forceDay = false)
    {
        if (isNight && !forceDay)
        {
            return;
        }

        if (!isNight)
        {
            currentNightTime = Time.time + nightDuration;

            // Until the machine is charged again
            if (Time.time < timeToCharge)
            {
                Debug.Log("Machine charging");
                return;
            }
        }
        else
        {
            timeToCharge = Time.time + nightCooldown;
        }

        isNight = !isNight;

        LightManager.Instance.ChangeMainLightColor(isNight);
    }
}
