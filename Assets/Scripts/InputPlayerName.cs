using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
    public class UserData {

    public string userNama; 

    }


public class InputPlayerName : MonoBehaviour
{
    string userName;
    public Button button;
    public TMP_InputField field;


    void Awake() {
        field.onValueChanged.AddListener(SetUserName);
        //field.onEndEdit.AddListener(SetUserName);
    }
    private void Start()
    {
        button.interactable = false;
        button.onClick.AddListener(SaveUserNameXML);

        /*button.enabled = !string.IsNullOrEmpty(userName);
        Debug.Log(string.IsNullOrEmpty(userName));*/
    }

    public void SetUserName(string text)
    {
        userName = text.ToString();
        button.interactable = !string.IsNullOrEmpty(userName);
    }

    private void SaveUserNameXML() {
        if (string.IsNullOrEmpty(userName)) {
            Debug.LogWarning("Empty name place"); 
            return;
        }

        UserData userData = new UserData();
        userData.userNama = userName;

        string folder = Path.Combine(Application.persistentDataPath, "User Data");
        if (!Directory.Exists(folder)) { 
            Directory.CreateDirectory(folder);
        }

        string filePath = Path.Combine(folder, "userData.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(UserData));
        using (FileStream stream = new FileStream(filePath, FileMode.Create)) { 
            serializer.Serialize(stream, userData);
        }

        Debug.Log("Name saved in file: " + filePath);
    }

}
