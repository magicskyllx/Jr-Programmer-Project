using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    public Color teamColor;

    private void Awake()
    {
        Debug.Log("MainManager Awake");
        if(instance != null)
        {
            Debug.Log("MainManager Destroy");
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadColor();
    }

    public void SaveColor()
    {
        SaveData saveData = new SaveData();
        saveData.teamColor = teamColor;

        string json = JsonUtility.ToJson(saveData);
        string path = Application.persistentDataPath + "/savefile.json";
        File.WriteAllText(path, json);
        Debug.Log("Save data file: " + path);
    }

    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        Debug.Log("Load data file: " + path);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            teamColor = saveData.teamColor;
        }
    }

    [System.Serializable]
    private class SaveData
    {
        public Color teamColor;
    }
}
