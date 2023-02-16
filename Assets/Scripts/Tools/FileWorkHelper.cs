using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class FileWorkHelper : MonoBehaviour
{
    public string readFile(string path)
    {
        return File.ReadAllText(path);
    }

    public ArrayList readFileLineByLine(string path)
    {
        ArrayList lines = new ArrayList();
        using (StreamReader reader = new StreamReader(path))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }
        }

        return lines;
    }

    public bool checkFileExists(string path)
    {
        return File.Exists(path);
    }

    public bool createFileIfNotExists(string path)
    {
        if (!File.Exists(path))
        {
            try
            {
                using (File.Create(path))
                {
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    public bool mkdir(string path)
    {
        try
        {
            Directory.CreateDirectory(path);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool appendToFile(string path, string line)
    {
        try
        {
            if (!File.Exists(path))
            {
                createFileIfNotExists(path);
            }

            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(line);
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool writeToFile(string path, string text)
    {
        try
        {
            File.WriteAllText(path, text);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}