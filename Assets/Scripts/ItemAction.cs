using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAction : MonoBehaviour
{
    //other gameobject 
    public GameObject BulletsFather;
    public GameController GameController;
    private ObjectsPool Pool;

    //self data
    public int index;

    public bool isLeft;
    public bool isGameStart;
    public Image GreenHalo;
    public Image BlackHalo;
    private Image Center;
    private Image Circle;

    //bullet data
    public BulletType Type;

    public float ShootSpeed;
    public int ways;
    public int TotalLife;
    public int CurrentLife;
    public List<int> angles;

    // Use this for initialization
    void Start()
    {
        if (isLeft)
        {
            Pool = GameObject.Find("UI/Bullets-left").GetComponent<ObjectsPool>();
        }
        else
        {
            Pool = GameObject.Find("UI/Bullets-right").GetComponent<ObjectsPool>();
        }

        Center = gameObject.GetComponent<Image>();
        Circle = gameObject.transform.GetChild(1).gameObject.GetComponent<Image>();

        Reset();
    }

    public void Init(BulletType type, int _index)
    {
        index = _index;
        Type = type;
        angles.Clear();

        switch (type)
        {
            case BulletType.a:
                ways = 2;
                ShootSpeed = 0.5f;
                TotalLife = CurrentLife = 180;
                break;
            case BulletType.b:
                ways = 3;
                ShootSpeed = 0.25f;
                TotalLife = CurrentLife = 200;
                break;

            default:
                break;
        }

        ConfigDirections();
    }

    void ConfigDirections()
    {
        if (ways == 1)
        {
            angles.Add(0);
        }
        else if (ways == 2)
        {
            angles.Add(10);
            angles.Add(-10);
        }
        else if (ways == 3)
        {
            angles.Add(15);
            angles.Add(0);
            angles.Add(-15);
        }
    }

    public void GameStart()
    {
        isGameStart = true;
        InvokeRepeating("Shoot", 0, ShootSpeed);
    }

    public void StopGame()
    {
        CancelInvoke();
        Reset();
    }

    void Shoot()
    {
        for (int i = 0; i < ways; i++)
        {
            GameObject bullet = Pool.GetInstance(Type);
            bullet.GetComponent<BulletAction>().Init(angles[i]);
            bullet.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position + transform.right*240 + transform.up * angles[i]*5;
        }
    }

    void Update()
    {
        if (Input.touchCount > index && isGameStart)
        {
            gameObject.GetComponent<RectTransform>().position = Input.touches[index].position;
        }

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Init(BulletType.b, 0);
        //    EnterFight();
        //    InvokeRepeating("Shoot", 0, ShootSpeed);
        //}
    }

    void Reset()
    {
        index = 10;
        isGameStart = false;
        BackToNormalStyle();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BulletAction b = other.GetComponent<BulletAction>();
        if (b == null)
            return;
        else
        {
            CurrentLife -= b.harm;
            b.OnHit();
            Center.color = new Color(1, 1, 1, (float) 0.6 * CurrentLife/ TotalLife + 0.2f);

            if (CurrentLife < 0)
            {
                GameController.StopGame();
            }
        }
    }

    #region style
    void BackToNormalStyle()
    {
        Circle.color = new Color(1, 1, 1, 0);
        Center.sprite = BlackHalo.sprite;
        Center.color = new Color(1,1,1,1);

        //todo pos
    }

    public void EnterReady()
    {
        Circle.color = new Color(1, 1, 1, 1);
    }

    public void EnterFight()
    {
        Center.sprite = GreenHalo.sprite;
        Center.color = new Color(1, 1, 1, 0.8f);
        GameStart();
    }


    #endregion


}