using System;
using System.IO;
using UnityEngine;

public class  LogWorkHelper : MonoBehaviour
{

     private static LogWorkHelper instance;
    private string logFilePath;
    private string lastLogFilePath;

    private void Awake()
    {
      
        logFilePath = Application.dataPath + "/log.txt";
        lastLogFilePath = Application.dataPath + "/lastlog.txt";

        if (File.Exists(lastLogFilePath))
        {
            File.Delete(lastLogFilePath);
        }
    }

    public static void Log(string msg)
    {
        string logMessage = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + msg;

        File.AppendAllText(instance.logFilePath, logMessage + Environment.NewLine);
        File.AppendAllText(instance.lastLogFilePath, logMessage + Environment.NewLine);
    }

    public static void DeleteLastLog()
    {
        if (File.Exists(instance.lastLogFilePath))
        {
            File.Delete(instance.lastLogFilePath);
        }
    }

    private void OnApplicationQuit()
    {
        File.Copy(logFilePath, lastLogFilePath, true);
    }
}