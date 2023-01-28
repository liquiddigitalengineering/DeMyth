using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName ="saving", menuName ="Saving/PersistantSaving")]
public class PersistantSaving : ScriptableObject
{
    [SerializeField] private List<ScriptableObject> scriptableObjects;

    private string DataSavingPath() => Path.Combine(Application.persistentDataPath, $"GameSave");
    private void CreateDataSavingDictionary() => Directory.CreateDirectory(DataSavingPath());

    string fileName;


    public void SaveData()
    {
        if (!Directory.Exists(DataSavingPath())) CreateDataSavingDictionary();

        for (int i = 0; i < scriptableObjects.Count; i++) {

            fileName = Path.Combine(DataSavingPath(), $"File{i}");

            BinaryFormatter bf = new();
            FileStream file = File.Create(fileName);
            var json = JsonUtility.ToJson(scriptableObjects[i]);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadData()
    {
        for (int i = 0; i < scriptableObjects.Count; i++) {
            fileName = Path.Combine(DataSavingPath(), $"File{i}");

            if (File.Exists(fileName)) {
                BinaryFormatter bf = new();
                FileStream file = File.Open(fileName, FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), scriptableObjects[i]);
                file.Close();
            }
            else
                Debug.Log("File not found");
        }
    }
}
