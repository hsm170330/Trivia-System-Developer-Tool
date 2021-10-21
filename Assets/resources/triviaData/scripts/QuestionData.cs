using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnswerTypes;

public class QuestionData : ScriptableObject
{
    public GameObject prefab;
    public string question;
    public string answer1;
    public string answer2;
    public string answer3;
    public string answer4;
    public AnswerType answerType;
}
