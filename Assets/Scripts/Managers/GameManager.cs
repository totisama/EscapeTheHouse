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
    private float currentNightTime;
    private float timeToCharge;
    private readonly List<Nocturnal> nocturnalList = new List<Nocturnal>();
    public bool IsNight { get; private set; }

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

        GameObject[] nocturnalGOs = GameObject.FindGameObjectsWithTag("Nocturnal");
        foreach (GameObject go in nocturnalGOs)
        {
            if (go != null)
            {
                nocturnalList.Add(go.GetComponent<Nocturnal>());
                go.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (IsNight && Time.time > currentNightTime)
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
        if (IsNight && !forceDay)
        {
            return;
        }

        if (!IsNight)
        {
            currentNightTime = Time.time + nightDuration;

            // Until the machine is charged again
            if (Time.time < timeToCharge)
            {
                return;
            }
        }
        else
        {
            timeToCharge = Time.time + nightCooldown;
        }

        IsNight = !IsNight;

        LightManager.Instance.ChangeMainLightColor(IsNight);

        foreach (Nocturnal nocturnalObject in nocturnalList)
        {
            if (nocturnalObject != null)
            {
                nocturnalObject.UpdateActive(IsNight);
            }
        }
    }
}
