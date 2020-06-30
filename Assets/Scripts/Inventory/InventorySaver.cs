using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class InventorySaver : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;


    private void OnEnable()
    {
        playerInventory.items.Clear();
        LoadScriptables();
    }


    private void OnDisable()
    {
        SaveScriptables();
    }


    public void SaveScriptables()
    {
        ResetScriptables();
        for (int i = 0; i < playerInventory.items.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.inv", i)); // create new file to save data
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(playerInventory.items[i]); // jsonify object i
            binary.Serialize(file, json); // save data to file
            file.Close();
        }
    }


    public void LoadScriptables()
    {
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
        {
            var loadedInventoryItem = ScriptableObject.CreateInstance<InventoryItem>();
            FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.inv", i), FileMode.Open); // open file i
            BinaryFormatter binary = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), loadedInventoryItem); // deserialize file i and save it to loadedInventoryItem
            playerInventory.items.Add(loadedInventoryItem);
            file.Close();
            i++;
        }
    }


    public void ResetScriptables()
    {
        int i = 0;
        while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
        {
            File.Delete(Application.persistentDataPath + string.Format("/{0}.inv", i));
            i++;
        }
    }
}
