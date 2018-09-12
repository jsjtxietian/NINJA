using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    private ObjectsPool Pool;
    public Sprite[] Sprites;
    private Image image;
    public bool NeedDestory;
    public string Path;
    public EndAction onEnd;
    public AudioSource Music;

    private int index = 0;

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
        if (!Path.Contains("VSEffect") && !Path.Contains("T01") && !Path.Contains("End"))
        {
            Pool = transform.parent.parent.gameObject.GetComponent<ObjectsPool>();
        }
        Sprites = Resources.LoadAll<Sprite>(Path);
        image = gameObject.GetComponent<Image>();
        if (Music != null)
        {
            Music.Play();
        }
    }

    void Update()
    {
        if (index < Sprites.Length)
        {
            image.sprite = Sprites[index];
            index++;
            return;
        }

        if(index == Sprites.Length && onEnd!= null)
        {
            onEnd();
        }

        if (index == Sprites.Length && NeedDestory)
        {
            Pool.ReturnInstance(BulletType.effect, gameObject);
        }
    }

    void OnDisable()
    {
        if (Music != null)
        {
            Music.Stop();
        }
    }
}