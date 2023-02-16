using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class INIWorkHelper : MonoBehaviour
{
    private void ReadIniFile(string FileName)
    {
        string filePath = Application.dataPath + "/" + FileName;
        if (!File.Exists(filePath))
        {
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            if (line.StartsWith(";"))
                continue;

            string[] keyValuePair = line.Split('=');
            if (keyValuePair.Length < 2)
                continue;

            string key = keyValuePair[0].Trim();
            string value = keyValuePair[1].Trim();
        }
    }

    public void WriteToIniFile(string FileName, string key, string value)
    {
        string filePath = Application.dataPath + "/" + FileName;
        string[] lines = File.ReadAllLines(filePath);
        bool keyFound = false;
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            if (line.StartsWith(key + "="))
            {
                lines[i] = key + "=" + value;
                keyFound = true;
                break;
            }
        }

        if (!keyFound)
        {
            Array.Resize(ref lines, lines.Length + 1);
            lines[lines.Length - 1] = key + "=" + value;
        }

        File.WriteAllLines(filePath, lines);
    }

    public string GetValue(string FileName, string key)
    {
        string filePath = Application.dataPath + "/" + FileName;
        if (!File.Exists(filePath))
        {
            Debug.LogError("Файла нет: " + filePath);
            return null;
        }

        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            if (line.StartsWith(key + "="))
            {
                string[] keyValuePair = line.Split('=');
                return keyValuePair[1].Trim();
            }
        }

        return null;
    }
}