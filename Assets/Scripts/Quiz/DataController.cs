using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    private GameRoundData[] allRoundsData;
    private PlayerProgress playerProgress;
    private string gameDataFileName = "Data.json";

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //SceneManager.LoadScene("MainMenu");

        LoadGameData();

        LoadPlayerProgress();
    }

    public GameRoundData GetCurrentGameRoundData() {
        return allRoundsData[0];
    }

    private void LoadPlayerProgress() {
        playerProgress = new PlayerProgress();

        if (PlayerPrefs.HasKey("highestScore")) {
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }

    private void SavePlayerProgress() {
        PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
    }

    public void SubmitNewPlayerScore(int newScore) {
        if (newScore > playerProgress.highestScore) { 
            playerProgress.highestScore = newScore;
            SavePlayerProgress();
        }
    }

    public int GetPlayerHighestScore() { 
        return playerProgress.highestScore;
    }

    private void LoadGameData() {
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            string dataAsJason = File.ReadAllText(filePath);

            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJason);

            allRoundsData = loadedData.allRoundsData;
        }
        else {
            Debug.LogError("Connot load game data");
        } 
    }

    private void Update()
    {
        
    }
}
