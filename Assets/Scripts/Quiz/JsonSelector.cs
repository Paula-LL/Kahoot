using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSelector : MonoBehaviour
{
    void Start() {
        ListAllJsonFiles();
    }

    void ListAllJsonFiles() {
        string folderPath = Path.Combine(Application.persistentDataPath, "StreamingAssets");

        if (!Directory.Exists(folderPath)) {
            Directory.CreateDirectory(folderPath);
        }

        string[] jsonFiles = Directory.GetFiles(folderPath, "*.json");

        if (jsonFiles.Length == 0) {
            Debug.Log("No jsons in folder");
        }
    }
}
