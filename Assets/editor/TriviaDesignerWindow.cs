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

    static QuestionData questionData;
    public static QuestionData QuestionInfo { get { return questionData; } }

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
        questionData = (QuestionData)ScriptableObject.CreateInstance(typeof(QuestionData));
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

        GUILayout.Label("Trivia Designer");

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
        GUILayout.Label("Number of Questions:");
        questionData.number = EditorGUILayout.FloatField(questionData.number);
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow();
        }

        GUILayout.EndArea();
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
}

public class GeneralSettings : EditorWindow
{
    static GeneralSettings window;
    bool inputsmade = false;

    public static void OpenWindow()
    {
        window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
        window.minSize = new Vector2(250, 200);
        window.Show();
    }

    void OnGUI()
    {
        DrawSettings((QuestionData)TriviaDesignerWindow.QuestionInfo);
    }

    void DrawSettings(QuestionData qData)
    {
        //
        //qData.question = new string[(int)qData.number];
        //qData.answer1 = new string[(int)qData.number];
        //qData.answer2 = new string[(int)qData.number];
        //qData.answer3 = new string[(int)qData.number];
        //qData.answer4 = new string[(int)qData.number];
        //qData.answerType1 = new AnswerType[(int)qData.number];
        //qData.answerType2 = new AnswerType[(int)qData.number];
        //qData.answerType3 = new AnswerType[(int)qData.number];
        //qData.answerType4 = new AnswerType[(int)qData.number];
        //
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Canvas");
        qData.canvas = (GameObject)EditorGUILayout.ObjectField(qData.canvas, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Question");
        qData.question = EditorGUILayout.TextField(qData.question);
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Answers");
        EditorGUILayout.BeginHorizontal();
        qData.answer1 = EditorGUILayout.TextField(qData.answer1);
        GUILayout.Label("Correct or Incorrect?");
        qData.answerType1 = (AnswerType)EditorGUILayout.EnumPopup(qData.answerType1);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        qData.answer2 = EditorGUILayout.TextField(qData.answer2);
        GUILayout.Label("Correct or Incorrect?");
        qData.answerType2 = (AnswerType)EditorGUILayout.EnumPopup(qData.answerType2);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        qData.answer3 = EditorGUILayout.TextField(qData.answer3);
        GUILayout.Label("Correct or Incorrect?");
        qData.answerType3 = (AnswerType)EditorGUILayout.EnumPopup(qData.answerType3);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        qData.answer4 = EditorGUILayout.TextField(qData.answer4);
        GUILayout.Label("Correct or Incorrect?");
        qData.answerType4 = (AnswerType)EditorGUILayout.EnumPopup(qData.answerType4);
        EditorGUILayout.EndHorizontal();


        if (qData.canvas == null)
        {
            EditorGUILayout.HelpBox("This Trivia needs a [canvas] before it can be created.", MessageType.Warning);
        }
        else if (GUILayout.Button("Finish and Save)", GUILayout.Height(30)))
        {
            SaveQuestionData();
            window.Close();
        }

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
