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

    public SimpleObjectPool answerButtonObjectPool;

    public Transform answerButtonParent; 

    public GameObject questionDisplay;
    public GameObject roundEndDisplay;


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
        currentRoundData = dataController.GetCurrentGameRoundData();
        questionPool = currentRoundData.questions;
        timeRemaining = currentRoundData.timeLimit;
        UpdateTimeRemeiningDisplay();

        playerScore = 0;
        questionIndex = 0;

        ShowQuestion();
        isRoundActive = true;
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

    private void AnswerButtonCLicked(bool isCorrect) { 
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

        questionDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("Menu"); /////////////////Change Scene
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