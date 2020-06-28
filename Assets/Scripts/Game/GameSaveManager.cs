using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager gameSave;
    public List<ScriptableObject> objects = new List<ScriptableObject>();


    private void Awake()
    {
        if (gameSave == null)
        {
            gameSave = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }


    private void OnEnable()
    {
        LoadScriptables();
    }


    private void OnDisable()
    {
        SaveScriptables();
    }


    public void SaveScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", i)); // create new file to save data
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]); // jsonify object i
            binary.Serialize(file, json); // save data to file
            file.Close();
        }
    }


    public void LoadScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
            {
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", i), FileMode.Open); // open file i
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), objects[i]); // deserialize file i and save it to objects i
                file.Close();
            }
        }
    }
}
