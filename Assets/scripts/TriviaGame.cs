using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Question))]
public class TriviaGame : MonoBehaviour
{
    public GameObject canvas;
    QuestionData questionData;
    public GameObject Question;
    public GameObject AnswerPanel;
    public GameObject StartPanel;
    public GameObject Correct, Incorrect;

    public Text a1, a2, a3, a4, q;
    int num = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (canvas.GetComponent<Question>().questionData == null)
        {
            Debug.Log("Question Data is null :p");
        }
        else
        {
            questionData = canvas.GetComponent<Question>().questionData;
            StartPanel.SetActive(true);
        }

    }

    public void StartTrivia()
    {
        StartPanel.SetActive(false);
        Question.SetActive(true);
        AnswerPanel.SetActive(true);

        q.text = questionData.question[num];
        a1.text = questionData.answer1[num];
        a2.text = questionData.answer2[num];
        a3.text = questionData.answer3[num];
        a4.text = questionData.answer4[num];

    }

    public void Answer1()
    {
        if (questionData.answerType1[num] == AnswerTypes.AnswerType.CORRECT)
        {
            Question.SetActive(false);
            AnswerPanel.SetActive(false);
            Correct.SetActive(true);
        }
        else if (questionData.answerType1[num] == AnswerTypes.AnswerType.INCORRECT)
        {
            Question.SetActive(false);
            AnswerPanel.SetActive(false);
            Incorrect.SetActive(true);
        }
    }

    public void Answer2()
    {
        if (questionData.answerType2[num] == AnswerTypes.AnswerType.CORRECT)
        {
            Question.SetActive(false);
            AnswerPanel.SetActive(false);
            Correct.SetActive(true);
        }
        else if (questionData.answerType2[num] == AnswerTypes.AnswerType.INCORRECT)
        {
            Question.SetActive(false);
            AnswerPanel.SetActive(false);
            Incorrect.SetActive(true);
        }
    }

    public void Answer3()
    {
        if (questionData.answerType3[num] == AnswerTypes.AnswerType.CORRECT)
        {
            Question.SetActive(false);
            AnswerPanel.SetActive(false);
            Correct.SetActive(true);
        }
        else if (questionData.answerType3[num] == AnswerTypes.AnswerType.INCORRECT)
        {
            Question.SetActive(false);
            AnswerPanel.SetActive(false);
            Incorrect.SetActive(true);
        }
    }

    public void Answer4()
    {
        if (questionData.answerType4[num] == AnswerTypes.AnswerType.CORRECT)
        {
            Question.SetActive(false);
            AnswerPanel.SetActive(false);
            Correct.SetActive(true);
        }
        else if (questionData.answerType4[num] == AnswerTypes.AnswerType.INCORRECT)
        {
            Question.SetActive(false);
            AnswerPanel.SetActive(false);
            Incorrect.SetActive(true);
        }
    }

    public void Next()
    {
        num++;
        if (num == questionData.question.Count)
        {
            StartPanel.SetActive(true);
            Question.SetActive(false);
            AnswerPanel.SetActive(false);
            Correct.SetActive(false);
            Incorrect.SetActive(false);

            num = 0;
        }
        else
        {
            q.text = questionData.question[num];
            a1.text = questionData.answer1[num];
            a2.text = questionData.answer2[num];
            a3.text = questionData.answer3[num];
            a4.text = questionData.answer4[num];

            StartPanel.SetActive(false);
            Question.SetActive(true);
            AnswerPanel.SetActive(true);
            Correct.SetActive(false);
            Incorrect.SetActive(false);
        }
    }
}
