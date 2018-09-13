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
    public AudioSource Shoot1Audio;
    public AudioSource Shoot2Audio;
    public AudioSource Explode;

    //self data
    public float hurtTime;

    public int index;
    private int fingerid;
    public Vector3 initPos;
    public bool isLeft;
    public bool isGameStart;
    public Image GreenHalo;
    public Image WhiteHalo;
    public Image RedHalo;
    private Image Center;
    private Image Circle;
    public GameObject IntroWord;
    private AudioSource SelfShootAudio;

    //bullet data
    public BulletType Type;

    public float ShootSpeed;
    public int ways;
    public int TotalLife;
    public int currentLife;
    public List<int> angles;

    public int CurrentLife
    {
        get { return currentLife; }
        set
        {
            currentLife = value;
            Center.sprite = RedHalo.sprite;
            Center.color = new Color(1, 1, 1, 0.8f * currentLife / TotalLife + 0.2f);
            StartCoroutine(BackToGreen());
        }
    }

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
        initPos = gameObject.GetComponent<RectTransform>().position;
        Reset();
    }

    public void Init(BulletType type, int _index)
    {
        index = _index;
        fingerid = Input.touches[index].fingerId;
        Type = type;
        angles.Clear();

        switch (type)
        {
            case BulletType.a:
                ways = 1;
                ShootSpeed = 0.2f;
                TotalLife = currentLife = 180;
                IntroWord.GetComponent<SpriteAnimator>().Path = "Sequence/VSBLK";
                SelfShootAudio = Shoot1Audio;
                break;
            case BulletType.b:
                ways = 2;
                ShootSpeed = 0.3f;
                TotalLife = currentLife = 200;
                IntroWord.GetComponent<SpriteAnimator>().Path = "Sequence/VSWHT";
                SelfShootAudio = Shoot2Audio;
                break;

            default:
                break;
        }

        ConfigDirections();
        gameObject.GetComponent<BreathEffect>().StopATBig();
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
        StopAllCoroutines();
        Pool.RecycleAll();
        Reset();
    }

    void Shoot()
    {
        SelfShootAudio.Play();
        for (int i = 0; i < ways; i++)
        {
            GameObject bullet = Pool.GetInstance(Type);
            bullet.GetComponent<BulletAction>().Init(angles[i]);
            bullet.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position +
                                                            transform.right * 240 + transform.up * angles[i] * 5;
        }
    }

    void Update()
    {
        if (isGameStart)
        {
            Vector3 newPos = GetPosByFingerId();
            if (!newPos.Equals(Vector3.back))
            {
                gameObject.GetComponent<RectTransform>().position = newPos;
            }
        }
    }

    Vector3 GetPosByFingerId()
    {
        foreach (var touch in Input.touches)
        {
            if (touch.fingerId == fingerid)
                return touch.position;
        }

        return Vector3.back;
    }

    void Reset()
    {
        index = 10;
        isGameStart = false;
        currentLife = TotalLife;

        BackToNormalStyle();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isGameStart)
        {
            BulletAction b = other.gameObject.GetComponent<BulletAction>();
            if (b == null)
                return;
            else
            {
                //boom effect
                var boom = Pool.GetInstance(BulletType.effect);
                boom.GetComponent<RectTransform>().position =
                    b.GetComponent<RectTransform>().position;

                //data part
                CurrentLife -= b.harm;

                //bullet part
                b.OnHit();

                //music part
                //Explode.Play();

                if (currentLife < 0)
                {
                    GameController.StopGame(Type);
                }
            }
        }
    }

    #region style

    void BackToNormalStyle()
    {
        Circle.color = new Color(1, 1, 1, 0);
        Center.sprite = WhiteHalo.sprite;
        Center.color = new Color(1, 1, 1, 1);

        gameObject.GetComponent<RectTransform>().position = initPos;
    }

    IEnumerator BackToGreen()
    {
        yield return new WaitForSeconds(hurtTime);
        Center.sprite = GreenHalo.sprite;
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