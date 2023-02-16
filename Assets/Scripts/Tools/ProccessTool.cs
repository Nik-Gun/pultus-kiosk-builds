using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ProccessTool : MonoBehaviour
{
    private string result = "";

    public void Proc()
    {
        Process process = new Process();
        process.StartInfo.FileName = @"C:\\30_0_18\\loadparm.bat";
        process.StartInfo.Arguments = "1 100";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();
        result = process.StandardOutput.ReadToEnd();
    }

    public void runProcess()
    {
        Thread myThread1 = new Thread(Proc);
        myThread1.Start();
    }

}