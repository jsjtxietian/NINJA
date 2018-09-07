using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Config : MonoBehaviour
{
    public List<string> RFIDs = new List<string>();

    void Start()
    {
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "Num.txt");

        using (StreamReader sr = new StreamReader(path))
        {
            string line;

            // 从文件读取并显示行，直到文件的末尾 
            while ((line = sr.ReadLine()) != null)
            {
                RFIDs.Add(line);
            }
        }

    }

}
