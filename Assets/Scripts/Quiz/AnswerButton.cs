using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
public class AnswerButton : MonoBehaviour   
{
    public TextMeshProUGUI answerText;
    private GameController gameController;
    private AnswerData answerData;


    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void SetUp(AnswerData data) { 
        answerData = data;
        answerText.text = answerData.answerText;
    }

    public void HandleClick() {
        gameController.AnswerButtonClicked(answerData.isCorrect);
    }
}
