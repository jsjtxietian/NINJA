using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDReader : MonoBehaviour
{
    private Config Config;
    private List<char> CurrentInputs = new List<char>();
    private char currentOne;

	// Use this for initialization
	void Start ()
	{
	    Config = gameObject.GetComponent<Config>();
	}

    void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                var key = e.keyCode;
                Debug.Log(key);
                if (key == KeyCode.Return)
                {
                    string s = new string(CurrentInputs.ToArray());
                    CurrentInputs.Clear();
                    currentOne = Char.MinValue;
                    AddRFID(s);
                }
                else if (key == KeyCode.None)
                {
                    CurrentInputs.Add(currentOne);
                }
                else
                {
                    currentOne = key.ToString().ToCharArray()[key.ToString().Length - 1];
                }
            }
        }
    }

    private void AddRFID(string rfid)
    {
        foreach (var prebuild in Config.RFIDs)
        {
            if (rfid.Equals(prebuild))
            {
                Debug.Log("success" + rfid);
            }
        }
    }
}
