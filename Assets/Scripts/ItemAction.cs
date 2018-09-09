using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAction : MonoBehaviour
{
    public GameObject BulletsFather;
    private ObjectsPool Pool;

    public int index = 10;
    public bool isLeft;
    public BulletType Type;
    public float ShootSpeed;
    public int ways;
    public int life;
    public List<Vector3> directions;

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
    }

    public void Init(BulletType type)
    {
        Type = type;
        directions.Clear();

        switch (type)
        {
            case BulletType.a:
                ways = 2;
                ShootSpeed = 0.5f;
                life = 180;
                break;
            case BulletType.b:
                ways = 3;
                ShootSpeed = 0.25f;
                life = 200;
                break;

            default:
                break;
        }

        ConfigDirections();
    }

    void ConfigDirections()
    {
        if(ways == 1)
            directions.Add(transform.up);
        else if (ways == 2)
        {
            directions.Add(Vector3.Normalize(new Vector3(1800, 200, 0)));
            directions.Add(Vector3.Normalize(new Vector3(1800, -200, 0)));
        }
        else if (ways == 3)
        {
            directions.Add(Vector3.Normalize(new Vector3(1800, 450, 0)));
            directions.Add(new Vector3(1, 0, 0));
            directions.Add(Vector3.Normalize(new Vector3(1800, -450, 0)));
        }
    }

    public void GameStart()
    {
        InvokeRepeating("Shoot",0, ShootSpeed);
    }

    public void StopGame()
    {
        CancelInvoke();
    }

    void Shoot()
    {
        for (int i = 0; i < ways; i++)
        {
            GameObject bullet = Pool.GetInstance(Type);
            bullet.GetComponent<BulletAction>().Init(transform.right);
            bullet.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position;
        }
    }

    void Update()
    {
        if (Input.touchCount > index)
        {
            gameObject.GetComponent<RectTransform>().position = Input.touches[index].position;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            GameStart();
        }
    }
}