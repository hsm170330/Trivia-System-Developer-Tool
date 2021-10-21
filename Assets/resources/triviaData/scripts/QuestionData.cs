using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnswerTypes;

public class QuestionData : ScriptableObject
{
    public GameObject canvas;
    public float number = 1;
    public string question;
    public string answer1;
    public string answer2;
    public string answer3;
    public string answer4;
    public AnswerType answerType1;
    public AnswerType answerType2;
    public AnswerType answerType3;
    public AnswerType answerType4;
}
