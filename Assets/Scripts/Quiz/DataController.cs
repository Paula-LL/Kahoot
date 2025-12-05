using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    private GameRoundData[] allRoundsData;
    private PlayerProgress playerProgress;
    public static string gameDataFileName = "Data.json";

    public static DataController Instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //SceneManager.LoadScene("MainMenu");

        LoadGameData();

        LoadPlayerProgress();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameRoundData GetCurrentGameRoundData() {
        return allRoundsData[playerProgress.currentRound];
    }

    private void LoadPlayerProgress() {
        playerProgress = new PlayerProgress();

        if (PlayerPrefs.HasKey("highestScore")) {
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
        }

        if (PlayerPrefs.HasKey("currentRound")) {
            playerProgress.currentRound = PlayerPrefs.GetInt("currentRound");
        }
    }

    private void SavePlayerProgress() {
        PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
    }

    private void SaveRoundProgress() {
        PlayerPrefs.SetInt("currentRound", playerProgress.currentRound);
    }

    public void ResetRoundProgress() {
        playerProgress.currentRound = 0;
        SaveRoundProgress();
    }

    public void GetNextRound() {
        if (HasMoreRounds()) {
            playerProgress.currentRound++;
            SaveRoundProgress();
        }
    }

    public bool HasMoreRounds() {
        return (allRoundsData.Length - 1 > playerProgress.currentRound);
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

    public void SetGameData(GameData newData)
    {
        allRoundsData = newData.allRoundsData;
        playerProgress.currentRound = 0;

        Debug.Log("Datos del JSON aplicados al juego");

    } 

    private void Update()
    {
        
    }
}
