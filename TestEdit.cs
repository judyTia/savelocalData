using System.Collections;
using UnityEditor;
using UnityEngine;
using System.IO;
public class TestEdit : EditorWindow
{
    public useTest gameData;

    private string gameDataProjectFilePath = "/StreamingAssets/mytest7.json";
    private bool issecond = true;
    [MenuItem("Window/TestEdit Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(TestEdit)).Show();
        
    }
    void OnGUI()
    {
        if(!issecond)
        {
            SaveAtFirst();
            issecond = true;
        }

        if (gameData != null)
        {
            
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("gameData");
            EditorGUILayout.PropertyField(serializedProperty, true);

            serializedObject.ApplyModifiedProperties();
            if (GUILayout.Button("Save data"))
            {
                SaveGameData();
            }
        }


        if (GUILayout.Button("Load data"))
        {
            LoadGameData();
        }
    }
    private void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<useTest>(dataAsJson);
        }
        else
        {
            gameData = new useTest();
        }
    }

    private void SaveGameData()
    {

        string dataAsJson = JsonUtility.ToJson(gameData);

        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);

    }
    private void SaveAtFirst()
    {
        useTest testData = new useTest();
        testData.mytest = new Test[4];
        for (int i = 0; i < 4; i++)
        {
            testData.mytest[i] = new Test();

            testData.mytest[i].p = 10;
            testData.mytest[i].c = Color.yellow;
        }
        string dataAsJson = JsonUtility.ToJson(testData);

        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);
    }
}
