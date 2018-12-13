using UnityEngine;
using UnityEditor;
using System.IO;

public class GameDataEditor : EditorWindow {

    public GameData gameData, flagData, transitionFactsData;

    private string gameDataFilePath = "/StreamingAssets/data.json";
    private string flagDataFilePath = "/StreamingAssets/flag.json";
    private string transitionFactsDataFilePath = "/StreamingAssets/transitionFacts.json";

    Vector2 scrollPosition = Vector2.zero;

    [MenuItem ("Window/Game Data Editor")]
    static void Init()
    {
        GameDataEditor window = (GameDataEditor)EditorWindow.GetWindow(typeof(GameDataEditor));
        window.Show();
    }

    void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true,
            GUILayout.Width(GetWindow<GameDataEditor>().position.width),
            GUILayout.Height(GetWindow<GameDataEditor>().position.height));

        SerializedObject serializedObject = new SerializedObject(this);

        if (gameData.allRoundData != null)
        {
            SerializedProperty serializedProperty1 = serializedObject.FindProperty("gameData.allRoundData");
            EditorGUILayout.PropertyField(serializedProperty1, true);
        }
        if (flagData.flagData != null)
        { 
            SerializedProperty serializedProperty2 = serializedObject.FindProperty("flagData.flagData");
            EditorGUILayout.PropertyField(serializedProperty2, true);
        }

        if (transitionFactsData.transitionFactData != null)
        {
            SerializedProperty serializedProperty3 = serializedObject.FindProperty("transitionFactsData.transitionFactData");
            EditorGUILayout.PropertyField(serializedProperty3, true);
            
        }
            serializedObject.ApplyModifiedProperties();
        if (GUILayout.Button("Save data"))
        {
            if (gameData != null)
            { SaveGameData(gameData, gameDataFilePath); }
            if (flagData != null)
            { SaveGameData(flagData, flagDataFilePath); }
            if (transitionFactsData != null)
            { SaveGameData(transitionFactsData, transitionFactsDataFilePath); }
        }

        if (GUILayout.Button("Load data"))
        {
            LoadGameData();
        }
        GUILayout.EndScrollView();
    }

    private void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataFilePath;
        string filePath2 = Application.dataPath + flagDataFilePath;
        string filePath3 = Application.dataPath + transitionFactsDataFilePath;
        // Load game data
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
        {
            gameData = new GameData();
        }
        // Load flag data
        if (File.Exists(filePath2))
        {
            string flagAsJson = File.ReadAllText(filePath2);
            flagData = JsonUtility.FromJson<GameData>(flagAsJson);
        }
        else
        {
            flagData = new GameData();
        }
        // Load transition facts data
        if (File.Exists(filePath3))
        {
            string tfAsJson = File.ReadAllText(filePath3);
            transitionFactsData = JsonUtility.FromJson<GameData>(tfAsJson);
        }
        else
        {
            transitionFactsData = new GameData();
        }
    }

    private void SaveGameData(GameData gd, string dataFilePath)
    {
        string jsonData = JsonUtility.ToJson(gd);
        string filePath = Application.dataPath + dataFilePath;
        File.WriteAllText(filePath, jsonData);
    }
}
