using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowTouch : MonoBehaviour
{
    public GameObject BulletsFather;
    private ObjectsPool Pool;

    //data
    public int index;

    public BulletType type;
    public float ShootSpeed;
    public int ways;

    // Use this for initialization
    void Start()
    {
        Pool = gameObject.GetComponent<ObjectsPool>();
    }

    public void Init(BulletType type)
    {
        ShootSpeed = 0.5f;
        ways = 1;
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
            GameObject bullet = Pool.GetInstance();
            bullet.GetComponent<BulletAction>().InitSelf(Pool);
            bullet.GetComponent<RectTransform>().SetParent(BulletsFather.transform);
            bullet.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position;
        }
    }

    void Update()
    {
        if (Input.touchCount > index)
        {
            gameObject.GetComponent<RectTransform>().position = Input.touches[index].position;
        }
    }
}