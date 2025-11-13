using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputPlayerName : MonoBehaviour
{
    public TMP_Text userName;
    public void SetUserName(string text)
    {
        userName.text = text.ToString(); 
    }
}
