using UnityEngine;
using UnityEngine.UI;

public class CloudController : MonoBehaviour
{
    public Sprite[] Sprites;
    private Image image;

    public string PathIn;
    public string PathLoop;

    private int index = 0;
    private bool isEnter;

    void Awake()
    {
        Init();
    }

    void OnEnable()
    {
        Init();
    }

    void Init()
    {
        index = 0;
        isEnter = true;
        Sprites = Resources.LoadAll<Sprite>(PathIn);
        image = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (!isEnter)
        {
            if (index < Sprites.Length)
            {
                image.sprite = Sprites[index];
                index++;
                return;
            }
            index = 0;
        }
        else
        {
            if (index < Sprites.Length)
            {
                image.sprite = Sprites[index];
                index++;
                return;
            }
            else
            {
                isEnter = false;
                Sprites = Resources.LoadAll<Sprite>(PathLoop);
                index = 0;
            }
        }
    }

}