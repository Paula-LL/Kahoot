using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    public GameRoundData[] allRoundsData;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public GameRoundData GetCurrentGameRoundData() {
        return allRoundsData[0];
    }

    private void Update()
    {
        
    }
}
