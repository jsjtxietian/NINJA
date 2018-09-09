using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFIDReader : MonoBehaviour
{
    public GameObject LeftItem;
    public GameObject RightItem;
    public bool isLeft;
    public bool isGameStart;

    private Config Config;
    private List<char> CurrentInputs = new List<char>();
    private char currentOne;

	// Use this for initialization
	void Start ()
	{
	    isGameStart = false;
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

    void Update()
    {
        if (!isGameStart)
        {
            if (Input.touchCount == 1)
            {
                isLeft = Input.touches[0].position.x > 960 ? false : true;
                if (isLeft)
                {
                    LeftItem.GetComponent<ItemAction>().index = 0;
                }
                else
                {
                    RightItem.GetComponent<ItemAction>().index = 0;
                }
            }

            if (Input.touchCount == 2)
            {
                if (isLeft)
                {
                    RightItem.GetComponent<ItemAction>().index = 1;
                }
                else
                {
                    LeftItem.GetComponent<ItemAction>().index = 1;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            LeftItem.GetComponent<ItemAction>().Init(BulletType.a);
            RightItem.GetComponent<ItemAction>().Init(BulletType.b);
        }
    }
}
