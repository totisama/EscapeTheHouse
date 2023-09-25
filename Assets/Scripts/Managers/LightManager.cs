using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    [SerializeField] private Light2D mainLight;
    [Header("Colors")]
    [SerializeField] private Color dayColor;
    [SerializeField] private Color nightColor;

    public static LightManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    public void ChangeMainLightColor(bool isNight)
    {
        mainLight.color = isNight ? nightColor : dayColor;
    }
}
