#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class SaveGameTutorialWindow : EditorWindow
{
    private Texture2D m_SoundSystemIcon;
    private GUIStyle m_HeaderStyle;
    private GUIStyle m_BodyStyle;
    private GUIStyle m_ImageStyle;
    private string m_BodyText;
    private Texture2D m_BackgroundTexture;
    private Vector2 m_ScrollPosition;

    private static bool m_IsOpen = false;

    private const string RESOURCE_PATH = "SaveGameInformation";

    public static void ShowWindow()
    {
        if (!m_IsOpen)
        {
            m_IsOpen = true;
            ForceShowWindow();

        }
    }

    [MenuItem("Window/Zindeaxx/Simple Saving/Information")]
    public static void ForceShowWindow ()
    {
        EditorWindow.GetWindow(typeof(SaveGameTutorialWindow));
        PlayerPrefs.SetInt("ZINDEAXX_SAVEGAME_INFOSHOW", 1);
    }


    private void CreateStyles()
    {
        if (m_SoundSystemIcon == null)
            m_SoundSystemIcon = Resources.Load<Texture2D>("SaveGameIcon");

        if (m_HeaderStyle == null)
        {
            m_HeaderStyle = new GUIStyle(EditorStyles.boldLabel);
            m_HeaderStyle.fontStyle = FontStyle.Bold;
            m_HeaderStyle.alignment = TextAnchor.MiddleCenter;
            m_HeaderStyle.normal.textColor = Color.white;
            m_HeaderStyle.fontSize = 30;
        }
        if (m_BodyStyle == null)
        {
            m_BodyStyle = new GUIStyle(EditorStyles.label);
            m_BodyStyle.wordWrap = true;
            m_BodyStyle.alignment = TextAnchor.MiddleCenter;
            m_BodyStyle.normal.textColor = Color.white;
            m_BodyStyle.fontSize = 16;
        }
        if (m_ImageStyle == null)
            m_ImageStyle = new GUIStyle(EditorStyles.label);

        if (m_BodyText == null)
            m_BodyText = Resources.Load<TextAsset>(RESOURCE_PATH).text;
        if (m_BackgroundTexture == null)
            m_BackgroundTexture = Resources.Load<Texture2D>("SaveGameBackground");

    }

    private void OnEnable()
    {
        this.minSize = new Vector2(Screen.width * 0.3f, Screen.height * 0.7f);
    }

    private void OnGUI()
    {
        CreateStyles();

        GUI.DrawTexture(new Rect(0, 0, position.width, position.height), m_BackgroundTexture, ScaleMode.StretchToFill);

        m_ScrollPosition = GUILayout.BeginScrollView(m_ScrollPosition);

        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.Box(m_SoundSystemIcon, m_ImageStyle);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        GUILayout.Label("Simple Saving", m_HeaderStyle);

        GUILayout.Space(20);

        GUILayout.Label(m_BodyText, m_BodyStyle, GUILayout.ExpandWidth(true));

        GUILayout.Space(20);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Visit my Asset Store"))
        {
            Application.OpenURL("https://assetstore.unity.com/publishers/53811");
        }

        if (GUILayout.Button("Close"))
        {
            PlayerPrefs.SetInt("ZINDEAXX_SAVEGAME_INFOSHOW", 1);
            Close();
        }
        GUILayout.EndHorizontal();

        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();

        GUILayout.EndScrollView();
    }
}
#endif
