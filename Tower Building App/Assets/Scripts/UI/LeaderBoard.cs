﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class LeaderBoard : MonoBehaviour
{
    public PlayerData data;
    
    private string file = "player.txt";
    public void Save()
    {
        string json = JsonUtility.ToJson(data);
        WriteToFile(file,json);
    }


    public void Load()
    {
        data = new PlayerData();
        string json = ReadFromFile(file);
        JsonUtility.FromJsonOverwrite(json,data);
    }

    private void WriteToFile(string fileName, string json)
    {
        string path = Application.persistentDataPath + "/" + fileName;
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    private string ReadFromFile(string fileName){
        string path = Application.persistentDataPath + "/" + fileName;
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Debug.LogWarning("Data Json file Not Found");
        }
        return "";
    }

}
