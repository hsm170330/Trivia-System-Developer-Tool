using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using AnswerTypes;

public class TriviaDesignerWindow : EditorWindow
{
    Texture2D headerSectionTexture;
    Texture2D mainSectionTexture;

    Color headerSectionColor = new Color(0f/255f, 125f/255f, 255f/255f, 1);

    Rect headerSection;
    Rect mainSection;

    public static QuestionData QuestionInfo { get; private set; }

    [MenuItem("Window/TriviaDesigner")]
    static void OpenWindow()
    {
        TriviaDesignerWindow window = (TriviaDesignerWindow)GetWindow(typeof(TriviaDesignerWindow));
        window.minSize = new Vector2(300, 300);
        window.Show();
    }

    /// <summary>
    /// Similar to Start() or Awake()
    /// </summary>
    void OnEnable()
    {
        InitTextures();
        InitData();
    }

    public static void InitData()
    {
        QuestionInfo = (QuestionData)CreateInstance(typeof(QuestionData));
        AddQuestion();
    }
    /// <summary>
    /// Adds a placeholder question in.
    /// </summary>
    public static void AddQuestion()
    {
        QuestionInfo.question.Add("Replace me");
        QuestionInfo.answer1.Add("Replace me");
        QuestionInfo.answer2.Add("Replace me");
        QuestionInfo.answer3.Add("Replace me");
        QuestionInfo.answer4.Add("Replace me");
        QuestionInfo.answerType1.Add(AnswerType.CORRECT);
        QuestionInfo.answerType2.Add(AnswerType.CORRECT);
        QuestionInfo.answerType3.Add(AnswerType.CORRECT);
        QuestionInfo.answerType4.Add(AnswerType.CORRECT);
    }

    /// <summary>
    /// Initialize Texture2D values
    /// </summary>
    void InitTextures()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0, 0, headerSectionColor);
        headerSectionTexture.Apply();

        mainSectionTexture = new Texture2D(1, 1);
        mainSectionTexture.SetPixel(0, 0, Color.white);
        mainSectionTexture.Apply();
    }

    /// <summary>
    /// Similar to any Update function
    /// Not called once per frame. Called 1 or more times per interaction.
    /// </summary>
    void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawQuestionSettings();
    }

    /// <summary>
    /// Defines Rect values and paints textures based on Rects
    /// </summary>
    void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 100;

        mainSection.x = 0;
        mainSection.y = headerSection.height;
        mainSection.width = Screen.width;
        mainSection.height = Screen.height;

        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(mainSection, mainSectionTexture);
    }

    /// <summary>
    /// Draw Contents of header
    /// </summary>
    void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);
        GUI.skin.label.fontSize = 25;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUI.skin.label.padding.top = 35;
        GUI.skin.label.fontStyle = FontStyle.Bold;
        GUILayout.Label("Trivia Designer");
        GUI.skin.label.fontSize = 16;
        GUI.skin.label.padding.top = 5;
        GUI.skin.label.padding.bottom = 10;
        GUILayout.EndArea();
    }
    /// <summary>
    /// Draw contents of Question region
    /// </summary>
    void DrawQuestionSettings()
    {
        GUILayout.BeginArea(mainSection);

        GUILayout.Label("Question Settings");

        EditorGUILayout.BeginHorizontal();
        GUI.skin.label.fontStyle = FontStyle.Normal;
        GUI.skin.label.padding.top = 0;
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;

        GUILayout.Label("Canvas:");
        QuestionInfo.canvas = (GameObject)EditorGUILayout.ObjectField(QuestionInfo.canvas, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Question #:", GUILayout.Width(120));
        if (GUILayout.Button("-", GUILayout.Height(20), GUILayout.Width(20)))
        {
            if (QuestionInfo.number > 1)
            {
                //remove the current item from each list with a remove button
                QuestionInfo.number--;
            }
        }

        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label(QuestionInfo.number + "", GUILayout.Width(25));

        if (GUILayout.Button("+", GUILayout.Height(20), GUILayout.Width(20)))
        {
            if (QuestionInfo.number == QuestionInfo.question.Count)
            {
                //add an empty string to each item in the list
                AddQuestion();

            }
            QuestionInfo.number++;
        }

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Edit", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow();
        }

        if (QuestionInfo.canvas == null)
        {
            EditorGUILayout.HelpBox("This Trivia needs a [canvas] before it can be created.", MessageType.Warning);
        }
        else if (GUILayout.Button("Finish and Save", GUILayout.Height(40)))
        {
            SaveQuestionData();
        }

        GUILayout.EndArea();

        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
        GUI.skin.label.padding.bottom = 0;
        GUI.skin.label.fontSize = 14;
        //Debug.Log(QuestionInfo.number);
        //Debug.Log(QuestionInfo.question.Count);
    }

    public static Button CreateButton(Button buttonPrefab, Canvas canvas, Vector2 cornerTopRight, Vector2 cornerBottomLeft)
    {
        var button = Object.Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity) as Button;
        var rectTransform = button.GetComponent<RectTransform>();
        rectTransform.SetParent(canvas.transform);
        rectTransform.anchorMax = cornerTopRight;
        rectTransform.anchorMin = cornerBottomLeft;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.offsetMin = Vector2.zero;
        return button;
    }
    void SaveQuestionData()
    {
        string prefabPath;
        string newPrefabPath = "Assets/prefabs/";
        string dataPath = "Assets/resources/triviaData/data/";

        dataPath += TriviaDesignerWindow.QuestionInfo.question + ".asset";
        AssetDatabase.CreateAsset(TriviaDesignerWindow.QuestionInfo, dataPath);
        newPrefabPath += "" + TriviaDesignerWindow.QuestionInfo.question + ".prefab";

        prefabPath = AssetDatabase.GetAssetPath(TriviaDesignerWindow.QuestionInfo.canvas);
        AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        GameObject canvasPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
        if (!canvasPrefab.GetComponent<Question>())
            canvasPrefab.AddComponent(typeof(Question));
        canvasPrefab.GetComponent<Question>().questionData = TriviaDesignerWindow.QuestionInfo;
    }
}

public class GeneralSettings : EditorWindow
{
    static GeneralSettings window;

    public static void OpenWindow()
    {
        window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
        window.minSize = new Vector2(250, 200);
        window.Show();
    }

    void OnGUI()
    {
        DrawSettings(TriviaDesignerWindow.QuestionInfo);
    }

    void DrawSettings(QuestionData qData)
    {
        
        //qData.question = new List<string>(2);
        //qData.answer1 = new List<string>(2);
        //qData.answer2 = new List<string>(2);
        //qData.answer3 = new List<string>(2);
        //qData.answer4 = new List<string>(2);
        //qData.answerType1 = new List<AnswerType>(2);
        //qData.answerType2 = new List<AnswerType>(2);
        //qData.answerType3 = new List<AnswerType>(2);
        //qData.answerType4 = new List<AnswerType>(2);

        //Debug.Log(qData.number);
        //Debug.Log(qData.question.Count);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Question:");
        qData.question[qData.number-1] = EditorGUILayout.TextField(qData.question[qData.number-1]);
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Answers:");
        EditorGUILayout.BeginHorizontal();
        qData.answer1[qData.number-1] = EditorGUILayout.TextField(qData.answer1[qData.number-1]);
        GUILayout.Label("Correct or Incorrect?");
        qData.answerType1[qData.number-1] = (AnswerType)EditorGUILayout.EnumPopup(qData.answerType1[qData.number-1]);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        qData.answer2[qData.number-1] = EditorGUILayout.TextField(qData.answer2[qData.number-1]);
        GUILayout.Label("Correct or Incorrect?");
        qData.answerType2[qData.number-1] = (AnswerType)EditorGUILayout.EnumPopup(qData.answerType2[qData.number-1]);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        qData.answer3[qData.number-1] = EditorGUILayout.TextField(qData.answer3[qData.number-1]);
        GUILayout.Label("Correct or Incorrect?");
        qData.answerType3[qData.number-1] = (AnswerType)EditorGUILayout.EnumPopup(qData.answerType3[qData.number-1]);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        qData.answer4[qData.number-1] = EditorGUILayout.TextField(qData.answer4[qData.number-1]);
        GUILayout.Label("Correct or Incorrect?");
        qData.answerType4[qData.number-1] = (AnswerType)EditorGUILayout.EnumPopup(qData.answerType4[qData.number-1]);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Save", GUILayout.Height(30)))
        {
            window.Close();
        }

    }
}
