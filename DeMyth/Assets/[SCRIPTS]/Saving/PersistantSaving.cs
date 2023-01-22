using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PersistantSaving : MonoBehaviour
{
    [SerializeField] private List<ScriptableObject> scriptableObjects;

    private void OnEnable()
    {
        LoadData();
    }

    public void SaveData()
    {
        for (int i = 0; i < scriptableObjects.Count; i++) {
            BinaryFormatter bf = new();
            FileStream file = File.Create(Application.persistentDataPath + $"File{i}");
            var json = JsonUtility.ToJson(scriptableObjects[i]);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadData()
    {
        for (int i = 0; i < scriptableObjects.Count; i++) {
            if (File.Exists(Application.persistentDataPath)) {
                BinaryFormatter bf = new();
                FileStream file = File.Open(Application.persistentDataPath + $"File{i}", FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), scriptableObjects[i]);
                file.Close();
            }
            else
                Debug.Log("File not found");
        }
    }
}
