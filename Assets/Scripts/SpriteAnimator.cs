using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    private ObjectsPool Pool;
    public Sprite[] Sprites;
    private Image image;
    public bool NeedDestory;
    public string Path;

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
        if (!Path.Contains("VSEffect"))
        {
            Pool = transform.parent.parent.gameObject.GetComponent<ObjectsPool>();
        }
        Sprites = Resources.LoadAll<Sprite>(Path);
        image = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (index < Sprites.Length)
        {
            image.sprite = Sprites[index];
            index++;
            return;
        }

        if (index == Sprites.Length && NeedDestory)
        {
            Pool.ReturnInstance(BulletType.effect, gameObject);
        }
    }
}