using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

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

    private GameObject LeftUI;
    private GameObject RightUI;

    //self data -- for game conrtol
    public BulletType currentOne;
    public bool isLeft;
    public bool isGameStart;
    public bool LeftStart;
    public bool RightStart;
    private int AddNum;

    void Start()
    {
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

    public void StopGame()
    {
        FightUI.SetActive(false);
        ReadyUI.SetActive(false);
        VSUI.SetActive(false);

        StandByUI.SetActive(true);

        //todo end ui
        StopAllCoroutines();
        LeftItem.StopGame();
        RightItem.StopGame();
        Reset();
    }

    public void Reset()
    {
        currentOne = BulletType.None;
        LeftStart = RightStart = false;
        AddNum = 0;
    }
}