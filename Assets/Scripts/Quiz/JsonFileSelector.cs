using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JsonFileSelector : MonoBehaviour
{
    public GameObject contentPanel;
    public Button buttonPrefab;

    private string folderPath;

    private void OnValidate()
    {
        Debug.Log("JsonFileSelector actiu a: " + gameObject.name);
    }

    void Start()
    {

        Debug.Log("Script Started");

        folderPath = Path.Combine(Application.streamingAssetsPath);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        ShowJsonFiles();
    }

    void ShowJsonFiles()
    {
        string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");

        foreach (string filePath in jsonFiles)
        {
            string fileName = Path.GetFileName(filePath);
            Button newButton = Instantiate(buttonPrefab, contentPanel.transform);
            //newButton.transform.SetParent(contentPanel.transform, false);
            newButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = fileName;

            newButton.onClick.AddListener(() =>
            {
                LoadJsonFiles(filePath);
            });

            Debug.Log("Button Created: " + fileName);

            Debug.Log("Button Parent: " + newButton.transform.parent);
        }
    }

    void LoadJsonFiles(string path)
    {
        string json = File.ReadAllText(path);

        GameData data = JsonUtility.FromJson<GameData>(json);

        Debug.Log("Loaded files" + path);
        Debug.Log("Loaded rounds" + data.allRoundsData.Length);

        DataController.Instance.SetGameData(data);
    }
}
