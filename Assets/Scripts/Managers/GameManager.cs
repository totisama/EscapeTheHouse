using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Machine machine;
    [SerializeField] CameraController cam;
    [SerializeField] RoomBounds startingRoom;
    [SerializeField] private TMP_Text[] mirrorTexts;
    [SerializeField] private GameObject finishUI;
    [Header("Night")]
    [Tooltip("In seconds")]
    [SerializeField] int nightDuration;

    private int nightCooldown;
    private float currentNightTime;
    private float timeToCharge;
    private string safeCode;
    private readonly List<Nocturnal> nocturnalList = new List<Nocturnal>();
    public bool IsNight { get; private set; }

    PlayerMovement playerMovement;
    Interact interact;
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
        finishUI.SetActive(false);
        startingRoom.UpdateRoomBounds();

        nightCooldown = machine.cooldown;

        GameObject[] nocturnalGOs = GameObject.FindGameObjectsWithTag("Nocturnal");
        foreach (GameObject go in nocturnalGOs)
        {
            if (go != null)
            {
                nocturnalList.Add(go.GetComponent<Nocturnal>());
                go.GetComponent<Nocturnal>().UpdateNocturnal(false);
            }
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            interact = player.GetComponent<Interact>();
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

        for (int i = 0; i < safeCode.Length; i++)
        {
            mirrorTexts[i].text = safeCode[i].ToString();
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

        machine.ChangeState(IsNight, Time.time < timeToCharge);

        LightManager.Instance.ChangeMainLightColor(IsNight);

        foreach (Nocturnal nocturnalObject in nocturnalList)
        {
            if (nocturnalObject != null)
            {
                nocturnalObject.UpdateNocturnal(IsNight);
            }
        }
    }

    public void TogglePlayer(bool value)
    {
        interact.canUseMachine = value;
        playerMovement.CanMove = value;
    }

    public bool CheckSafeCode(string code)
    {
        return safeCode == code;
    }

    public void FinishGame()
    {
        TogglePlayer(false);
        finishUI.SetActive(true);
    }
}
