using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputPlayerName : MonoBehaviour
{
    string userName;
    public Button button;
    public TMP_InputField field;

    private void Start()
    {
        button.enabled = !string.IsNullOrEmpty(userName);
        Debug.Log(string.IsNullOrEmpty(userName));
    }

    public void SetUserName(string text)
    {
        userName = text.ToString();
        button.enabled = !string.IsNullOrEmpty(userName);
    }

    private void Awake()
    {
        field.onEndEdit.AddListener(SetUserName);
    }

}
