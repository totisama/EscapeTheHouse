using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{
    [SerializeField] GameObject pad;

    private void Start()
    {
        pad.SetActive(false);
    }

    public void OpenPad()
    {
        pad.SetActive(true);
    }
}
