using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI questionDisplayText; 
    public TextMeshProUGUI scoreDisplayText;
    public TextMeshProUGUI timeRemainingDisplayText;
    public TextMeshProUGUI highScoreDisplay;

    public SimpleObjectPool answerButtonObjectPool;

    public Transform answerButtonParent; 

    public GameObject questionDisplay;
    public GameObject roundEndDisplay;
    public GameObject nextRoundDisplay;


    private DataController dataController;
    private GameRoundData currentRoundData;
    private QuestionData[] questionPool;

    private bool isRoundActive;
    private float timeRemaining;
    private int questionIndex;
    private int playerScore; 
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();


    private void Start()
    {
        dataController = FindObjectOfType<DataController> ();
        SetUpRound();
    }

    private void ShowQuestion() {
        RemoveAnswerButtons();
        QuestionData questionData = questionPool[questionIndex];
        questionDisplayText.text = questionData.questionText;

        for (int i = 0; i < questionData.answers.Length; i++) {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetGameObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.SetUp(questionData.answers[i]);
        }
    }

    private void RemoveAnswerButtons() {
        while (answerButtonGameObjects.Count > 0) {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool isCorrect) { 
        if (isCorrect)
        {
            playerScore += currentRoundData.pointsCorrectAnswers;
            scoreDisplayText.text = "Score: " + playerScore.ToString();
        }

        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else {
            EndRound();
        }
    }

    public void EndRound() {
        isRoundActive = false;

        dataController.SubmitNewPlayerScore(playerScore);
        highScoreDisplay.text = dataController.GetPlayerHighestScore().ToString();

        questionDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);    
        
        if (dataController.HasMoreRounds())
        {
            nextRoundDisplay.SetActive(true);
        }
        else {
            nextRoundDisplay.SetActive(false);
        }
    }

    public void GoToNextRound() {
        dataController.GetNextRound();
        SetUpRound();
        questionDisplay.SetActive(true);
        roundEndDisplay.SetActive(false);
    }

    public void SetUpRound()
    {
        currentRoundData = dataController.GetCurrentGameRoundData();
        questionPool = currentRoundData.questions;
        timeRemaining = currentRoundData.timeLimit;
        UpdateTimeRemeiningDisplay();

        playerScore = 0;
        questionIndex = 0;

        ShowQuestion();
        isRoundActive = true;
    }


    public void ReturnToMenu() {
        dataController.ResetRoundProgress();
        SceneManager.LoadScene("KahootSelector");
    }

    public void UpdateTimeRemeiningDisplay() { 
        timeRemainingDisplayText.text = "Time " + Mathf.Round (timeRemaining).ToString();
    }

    private void Update()
    {
        if (isRoundActive) { 
            timeRemaining -= Time.deltaTime;
            UpdateTimeRemeiningDisplay();

            if (timeRemaining <= 0) { 
                EndRound() ;
            }
        }
    }
}