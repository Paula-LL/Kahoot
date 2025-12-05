using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonDebugCreator : MonoBehaviour
{
    private void Start()
    {

        GameRoundData newPocho = new GameRoundData();
        newPocho.questions = new QuestionData[2];
        newPocho.questions[0] = new QuestionData();
        newPocho.questions[1] = new QuestionData();
        newPocho.questions[0].answers = new AnswerData[4];
        newPocho.questions[1].answers = new AnswerData[4];
        string json = JsonUtility.ToJson(newPocho);

        Debug.Log(json);

    }
}
