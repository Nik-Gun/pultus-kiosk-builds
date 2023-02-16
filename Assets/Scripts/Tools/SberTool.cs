using System;
using System.Diagnostics;
using System.IO;

public class SberTool
{
    public SberTool()
    {
    }

    public string payViaLoadParm(int price)
    {
        string result = "no";

        try
        {
            Process process = new Process();
            process.StartInfo.FileName = @"C:\\30_0_18\\loadparm.bat";
            // process.StartInfo.FileName = @"/Users/nikolay/trunks/UnityProjects/pultus-kiosk/Assets/loadparm.sh";
            process.StartInfo.Arguments = "1 " + (price * 100);
            process.StartInfo.UseShellExecute = false;
            // process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            // result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            StreamReader streamReader = new StreamReader("C:\\30_0_18\\result.txt");
            string str = "";
            while (!streamReader.EndOfStream)
            {
                str += streamReader.ReadLine();
            }

            streamReader.Close();
            result = str.Trim();
        }
        catch (Exception e)
        {
            result = "error:" + e.Message;
        }

        return result;
    }
}