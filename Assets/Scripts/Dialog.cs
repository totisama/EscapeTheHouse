using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private int dialogTextFont;
    [SerializeField] private string[] dialogs;
    [SerializeField] private string tagToSearch;
    [SerializeField] private bool alwaysShowDialog;
    [Header("Typing")]
    [SerializeField] private float typingSpeed;
    [SerializeField] private float nextDialog;

    private int currentDialog;
    private bool opened;

    private void Start()
    {
        dialogPanel.SetActive(false);
        dialogText.text = string.Empty;
    }

    private void OpenDialog()
    {
        if (!alwaysShowDialog)
        {
            opened = true;
        }
        dialogText.fontSize = dialogTextFont;
        dialogPanel.SetActive(true);
        GameManager.Instance.TogglePlayer(false);
        StartCoroutine(Typing());
    }

    private void ResetDialog()
    {
        dialogText.text = "";
        currentDialog = 0;
        dialogPanel.SetActive(false);
        GameManager.Instance.TogglePlayer(true);
    }

    private void NextDialog()
    {
        if (currentDialog < dialogs.Length - 1)
        {
            currentDialog += 1;
            dialogText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ResetDialog();
        }
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogs[currentDialog].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(nextDialog);
        NextDialog();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!opened && collision.CompareTag(tagToSearch))
        {
            OpenDialog();
        }
    }
}
