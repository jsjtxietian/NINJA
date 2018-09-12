using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    //gameobject
    public ItemAction LeftItem;

    public ItemAction RightItem;
    public GameObject StandByUI;
    public GameObject ReadyUI;
    public GameObject FightUI;
    public GameObject BG;
    public GameObject VSUI;
    public GameObject EndUI;

    public List<string> EndUIPath;

    private GameObject LeftUI;
    private GameObject RightUI;

    //self data -- for game conrtol
    public BulletType currentOne;

    public bool isLeft;
    public bool LeftStart;
    public bool RightStart;
    private int AddNum;

    void Start()
    {
        Application.targetFrameRate = 24;
        Cursor.visible = false;

        Reset();
        LeftUI = ReadyUI.transform.GetChild(0).gameObject;
        RightUI = ReadyUI.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentOne != BulletType.None && AddNum <= 2)
        {
            if (Input.touches.Length == 1 && AddNum == 0)
            {
                isLeft = Input.touches[0].position.x > 960 ? false : true;
                ItemAction item = isLeft ? LeftItem : RightItem;
                item.Init(currentOne, 0);
                currentOne = BulletType.None;
                AddNum++;

                PartReadyScene();
            }

            else if (Input.touches.Length == 2 && AddNum == 1)
            {
                ItemAction item = isLeft ? RightItem : LeftItem;
                item.Init(currentOne, 1);
                currentOne = BulletType.None;
                AddNum++;
                AllReady();
            }
        }
    }

    private void PartReadyScene()
    {
        StandByUI.SetActive(false);
        ReadyUI.SetActive(true);
        VSUI.SetActive(true);

        BG.GetComponent<BGController>().SwitchToLoop();

        if (isLeft)
        {
            LeftItem.EnterReady();
            LeftUI.SetActive(true);
        }
        else
        {
            RightItem.EnterReady();
            RightUI.SetActive(true);
        }
    }

    private void AllReady()
    {
        if (!isLeft)
        {
            LeftItem.EnterReady();
            LeftUI.SetActive(true);
        }
        else
        {
            RightItem.EnterReady();
            RightUI.SetActive(true);
        }
    }

    public void StartButtonLeft()
    {
        LeftStart = true;
        if (RightStart == true)
        {
            StartGame();
        }
    }

    public void StartButtonRight()
    {
        RightStart = true;
        if (LeftStart == true)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        LeftUI.SetActive(false);
        RightUI.SetActive(false);

        FightUI.SetActive(true);

        LeftItem.EnterFight();
        RightItem.EnterFight();
    }

    public void StopGame(BulletType type)
    {
        FightUI.SetActive(false);
        ReadyUI.SetActive(false);
        VSUI.SetActive(false);

        StopAllCoroutines();
        LeftItem.StopGame();
        RightItem.StopGame();
        Reset();

        EndUI.GetComponent<SpriteAnimator>().onEnd = () =>
        {
            BG.GetComponent<BGController>().SwitchToIntro();
            StandByUI.SetActive(true);
            EndUI.SetActive(false);
            LeftItem.gameObject.SetActive(true);
            RightItem.gameObject.SetActive(true);
        };
        EndUI.GetComponent<SpriteAnimator>().Path = GetPath(type);
        EndUI.SetActive(true);
        LeftItem.gameObject.SetActive(false);
        RightItem.gameObject.SetActive(false);
    }

    private string GetPath(BulletType type)
    {
        //a 冥角 b金角
        System.Random r = new System.Random();
        int a = r.Next(3);
        if (a == 0)
        {
            if (type == BulletType.b)
            {
                a = 1;
            }
        }
        else
        {
            a += 1;
        }

        return EndUIPath[a];
    }

    public void Reset()
    {
        currentOne = BulletType.None;
        LeftStart = RightStart = false;
        AddNum = 0;
    }
}