using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //gameobject
    public GameObject LeftItem;
    public GameObject RightItem;
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
        currentOne = BulletType.None;
        LeftStart = RightStart = false;
        AddNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentOne != BulletType.None && AddNum <= 2)
        {
            if (Input.touches.Length == 1 && AddNum == 0)
            {
                isLeft = Input.touches[0].position.x > 960 ? false : true;
                GameObject item = isLeft ? LeftItem : RightItem;
                item.GetComponent<ItemAction>().Init(currentOne, 0);
                currentOne = BulletType.None;
                AddNum++;
            }
            else if (Input.touches.Length == 2 && AddNum == 1)
            {
                GameObject item = isLeft ? RightItem : LeftItem;
                item.GetComponent<ItemAction>().Init(currentOne, 1);
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
        LeftItem.GetComponent<ItemAction>().EnterReady();
        RightItem.GetComponent<ItemAction>().EnterReady();
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

        LeftItem.GetComponent<ItemAction>().EnterFight();
        RightItem.GetComponent<ItemAction>().EnterFight();
    }
}