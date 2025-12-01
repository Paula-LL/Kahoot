using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Globalization;
using System;

public class GameDataEditor : EditorWindow
{
    public GameData gameData;

    //private string gameDataProjectDirectoryPath = "JsonAssets";

    [MenuItem("Window/ Game Data Editor")]

    static void Init()
    {
        EditorWindow.GetWindow(typeof(GameDataEditor)).Show();
    }

    private void OnGUI()
    {
        if (gameData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("gameData");
            EditorGUILayout.PropertyField(serializedProperty, true);

            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("SaveData"))
            {
                SaveGameData();
            }

        }

        if (GUILayout.Button("Load Data"))
        {
            LoadGameData();
        }
    }

    private void LoadGameData()
    {
        string filePath = GetJsonFilePath();

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
        {
            gameData = new GameData();
        }
    }

    private void SaveGameData()
    {
        string dataAsJson = JsonUtility.ToJson(gameData);

        if (DirectoryExists(Application.streamingAssetsPath, true))
        {
            if (DirectoryExists(GetJsonDirectoryPath(), true))
            {
                string filePath = GetJsonFilePath();
                File.WriteAllText(filePath, dataAsJson);
            }
        }
    }

    string GetJsonDirectoryPath()
    {
        return Application.streamingAssetsPath;
    }
    string GetJsonFilePath()
    {
        return Path.Combine(GetJsonDirectoryPath(), DataController.gameDataFileName) ;
    }

    private bool DirectoryExists(string path, bool forceCreate)
    {
        if (forceCreate)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        return Directory.Exists(path);
    }
}
