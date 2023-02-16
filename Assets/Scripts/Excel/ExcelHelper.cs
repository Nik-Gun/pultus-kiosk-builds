using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using UnityEngine;
using System.Text;
using Random = System.Random;

public class ExcelHelper : MonoBehaviour
{
    public Film f;

    public ExcelHelper()
    {
    }
    

    public string parse()
    {
        // Debug.Log("1");
        // WorkBook workbook = WorkBook.Load(@"Assets/Excel/Data.xlsx");
        // Debug.Log(workbook.WorkSheets.Count);
        return GetExcelDataReaderForFile(Application.persistentDataPath+"/"+"data.xlsx");
    }


    private string GetExcelDataReaderForFile(string filePath)
    {
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        try
        {
            // Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            List<string> lang = new List<string>();
            Dictionary<string, int> columnIndexOfName = new Dictionary<string, int>();
            for (int col = 0; col < excelReader.AsDataSet().Tables[0].Columns.Count; col++)
            {
                if (excelReader.AsDataSet().Tables[0].Rows[0].ItemArray[col].ToString().StartsWith("ID_"))
                    lang.Add(excelReader.AsDataSet().Tables[0].Rows[0].ItemArray[col].ToString().Replace("ID_", ""));
                columnIndexOfName.Add(excelReader.AsDataSet().Tables[0].Rows[0].ItemArray[col].ToString(), col);
            }

            int j = 0;
            foreach (DataRow line in excelReader.AsDataSet().Tables[0].Rows)
            {
                if (j != 0)
                {
                    Film f = new Film(j-1, lang.ToArray(),
                        line.ItemArray[columnIndexOfName["YEAR"]].ToString(),
                        line.ItemArray[columnIndexOfName["AGE"]].ToString(),
                        line.ItemArray[columnIndexOfName["OWNER"]].ToString(),
                        int.Parse(line.ItemArray[columnIndexOfName["PRICE"]] as string),
                        line.ItemArray[columnIndexOfName["DURATION"]].ToString()
                    );

                    for (int i = 0; i < line.ItemArray.Length; i++)
                    {
                        string cell = line.ItemArray[i].ToString();
                        string columnName = excelReader.AsDataSet().Tables[0].Rows[0].ItemArray[i].ToString();
                        foreach (var l in lang)
                        {
                            if (columnName.EndsWith(l) && !cell.Equals("-"))
                            {
                                switch (columnName.Split("_")[0])
                                {
                                    case "ID":
                                        f.ids.Add(l, int.Parse(cell));
                                        break;
                                    case "NAME":
                                        f.names.Add(l, cell);
                                        break;
                                    case "DESCRIPTION":
                                        f.descriptions.Add(l, cell);
                                        break;
                                    case "COVER":
                                        f.cover_paths.Add(l, cell);
                                        break;
                                }
                            }
                        }
                    }

                    FilmsManager.getInstance().getFilms().Add(f);
                }

                j++;
                //;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        Debug.Log("films count " + FilmsManager.getInstance().getFilms().Count);
        // Close the stream
        try
        {
            stream.Close();
        }
        catch (Exception e)
        {
            // ignored
        }

        return FilmsManager.getInstance().getFilms().Count + "";
    }
}