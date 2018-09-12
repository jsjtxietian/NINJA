using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDReader : MonoBehaviour
{
    public GameObject LeftItem;
    public GameObject RightItem;

    private Config Config;
    private List<char> CurrentInputs = new List<char>();
    private char currentOne;
    private GameController GameController;

	// Use this for initialization
	void Start ()
	{
        Config = gameObject.GetComponent<Config>();
	    GameController = gameObject.GetComponent<GameController>();
	}

    void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                var key = e.keyCode;
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
        int num = -1;

        for (int i = 0; i < Config.RFIDs.Count; i++)
        {
            if (rfid.Equals(Config.RFIDs[i]))
            {
                num = i;
            }
        }

        Debug.Log(rfid + "  " + num);

        if (num == 0)
        {
            GameController.currentOne = BulletType.a;
        }
        else if (num == 1)
        {
            GameController.currentOne = BulletType.b;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameController.currentOne = BulletType.a;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            GameController.currentOne = BulletType.b;
        }
    }
}
