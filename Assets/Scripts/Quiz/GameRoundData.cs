using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameRoundData
{
    public string gameRoundName;
    public int timeLimit;
    public int pointsCorrectAnswers;
    public QuestionData[] questions;
}
