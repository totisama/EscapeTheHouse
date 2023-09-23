using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Safe : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text codeText;
    [SerializeField] private Sprite openedSprite;
    [Header("Objects")]
    [SerializeField] private GameObject pad;
    [SerializeField] private GameObject key;
    [Header("Code")]
    [SerializeField] private Color wrongColor;
    [SerializeField] private Color correctColor;

    private string code;
    private bool blocked;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        pad.SetActive(false);
        codeText.text = "";
        key.SetActive(false);
    }

    public void OpenPad()
    {
        bool active = pad.activeInHierarchy;
        pad.SetActive(!active);
        GameManager.Instance.TogglePlayer(active);
    }

    public void AddCodeNumer(int number)
    {
        if (blocked || number < 0 || number > 9)
        {
            return;
        }

        code += number.ToString();
        codeText.text = code;

        if (code.Length >= 3)
        {
            bool correct = GameManager.Instance.CheckSafeCode(code);

            if (correct)
            {
                CorrectCode();
            }
            else
            {
                StartCoroutine(WrongCode());
            }
        }
    }

    private void CorrectCode()
    {
        blocked = true;
        codeText.color = correctColor;
        sr.sprite = openedSprite;
        sr.sortingOrder = 0;
        key.SetActive(true);
    }

    IEnumerator WrongCode()
    {
        blocked = true;
        codeText.color = wrongColor;
        yield return new WaitForSeconds(0.5f);
        codeText.color = Color.black;
        code = "";
        codeText.text = code;
        blocked = false;
    }
}
