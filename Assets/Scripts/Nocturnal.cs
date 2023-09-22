using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nocturnal : MonoBehaviour
{
    public void UpdateActive(bool isNight)
    {
        gameObject.SetActive(isNight);
    }
}
