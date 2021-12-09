using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnswerTypes;

public class QuestionData : ScriptableObject
{
    public GameObject canvas;
    public int number = 1;
    public List<string> question = new List<string>(1);
    public List<string> answer1 = new List<string>(1);
    public List<string> answer2 = new List<string>(1);
    public List<string> answer3 = new List<string>(1);
    public List<string> answer4 = new List<string>(1);
    public List<AnswerType> answerType1 = new List<AnswerType>(1);
    public List<AnswerType> answerType2 = new List<AnswerType>(1);
    public List<AnswerType> answerType3 = new List<AnswerType>(1);
    public List<AnswerType> answerType4 = new List<AnswerType>(1);
}
