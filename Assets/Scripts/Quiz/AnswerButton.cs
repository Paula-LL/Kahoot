using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
public class AnswerButton : MonoBehaviour   
{
    public TextMeshProUGUI answerText;
    private AnswerData answerData;

    public void SetUp(AnswerData data) { 
        answerData = data;
        answerText.text = answerData.answerText;
    }
}
