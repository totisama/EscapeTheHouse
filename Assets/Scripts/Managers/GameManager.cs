using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] CameraController cam;
    [SerializeField] RoomBounds startingRoom;
    [SerializeField] PlayerMovement playerMovement;
    [Header("Night")]
    [Tooltip("In seconds")]
    [SerializeField] int nightDuration;
    [Tooltip("In seconds")]
    [SerializeField] int nightCooldown;

    private float currentNightTime;
    private float timeToCharge;
    private string safeCode;
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

        CreateRandomSafeCode();
    }

    private void Update()
    {
        if (IsNight && Time.time > currentNightTime)
        {
            ChangeNight(true);
        }
    }
    private void CreateRandomSafeCode()
    {
        int randomNumber = Random.Range(100, 999);

        safeCode = randomNumber.ToString();
        Debug.Log(randomNumber);
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

    public void TogglePlayer(bool canMove)
    {
        playerMovement.CanMove = canMove;
    }

    public bool CheckSafeCode(string code)
    {
        return safeCode == code;
    }
}
