using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Machine : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [Tooltip("In seconds")]
    public int cooldown;
    [Header("Colors")]
    [SerializeField] private Color on;
    [SerializeField] private Color off;

    private bool charging;
    private float timeToUseAgain;

    void Start()
    {
        sr.color = on;
    }

    private void Update()
    {
        if (!charging)
        {
            return;
        }

        if (Time.time > timeToUseAgain)
        {
            ChangeState(false, false);
        }
    }

    public void ChangeState(bool isNight, bool chargingParam)
    {
       if (isNight)
       {
            sr.color = off;
       }
       else if (!charging && chargingParam)
       {
            charging = chargingParam;
            timeToUseAgain = Time.time + cooldown;
       }
       else
       {
            charging = false;
            sr.color = on;
       }
    }
}
