using UnityEngine;
using UnityEditor;
using System.IO;

public class GameDataEditor : EditorWindow {

    public GameData gameData;

    private string gameDataFilePath = "/StreamingAssets/data.json";

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
        if (gameData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("gameData");

            EditorGUILayout.PropertyField(serializedProperty, true);

            serializedObject.ApplyModifiedProperties();

            if(GUILayout.Button("Save data"))
            {
                SaveGameData();
            }
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

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else
        {
            gameData = new GameData();
        }
    }

    private void SaveGameData()
    {
        string dataAsJson = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + gameDataFilePath;
        File.WriteAllText(filePath, dataAsJson);

    }

}
