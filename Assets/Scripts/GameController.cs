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
            }
            else if (Input.touches.Length == 2 && AddNum == 1)
            {
                ItemAction item = isLeft ? RightItem : LeftItem;
                item.Init(currentOne, 1);
                currentOne = BulletType.None;
                AddNum++;
                EnterReadyScene();
            }
        }

    }

    private void EnterReadyScene()
    {
        StandByUI.SetActive(false);
        ReadyUI.SetActive(true);
        LeftItem.EnterReady();
        RightItem.EnterReady();
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
        ReadyUI.SetActive(false);
        FightUI.SetActive(true);

        LeftItem.EnterFight();
        RightItem.EnterFight();
    }

    public void StopGame()
    {
        LeftItem.StopGame();
        RightItem.StopGame();
        Reset();
    }

    public void Reset()
    {
        currentOne = BulletType.None;
        LeftStart = RightStart = false;
        AddNum = 0;

        //end animation
        StandByUI.SetActive(true);
    }
}