using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEngine.Networking;
using System.Diagnostics;
using UnityEngine.Video;
public class ChekVideo : MonoBehaviour
{
    public GameObject s1,s2;
    public VideoPlayer videoPlayer;
    public INIWorkHelper INIWorkHelper;
    string filmname;
    // Start is called before the first frame update
    void Start()
    {
        filmname=INIWorkHelper.GetValue("settings.ini","screensaver");
        UnityEngine.Debug.Log(filmname);
        videoPlayer.url=filmname;
        videoPlayer.Play();
        if(File.Exists("С"+"/"+filmname))
        {
       
        s2.SetActive(true);
        }
        else

        {
        s1.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
