using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    public GameObject BulletFather;
    private Dictionary<BulletType, Queue<GameObject>> Pool = new Dictionary<BulletType, Queue<GameObject>>();

    void Start()
    {
        AddTypeObjects(BulletType.a);
        AddTypeObjects(BulletType.b);
        AddTypeObjects(BulletType.effect);
    }

    void AddTypeObjects(BulletType type)
    {
        Pool.Add(type,new Queue<GameObject>());
        Transform ObjectsFather = BulletFather.transform.Find("Collection-" + type.ToString());
        for (int i = 0; i < ObjectsFather.childCount; i++)
        {
            Pool[type].Enqueue(ObjectsFather.GetChild(i).gameObject);
        }
    }

    public void RecycleAll()
    {
        Recycle(BulletType.a);
        Recycle(BulletType.b);
        Recycle(BulletType.effect);
    }

    void Recycle(BulletType type)
    {
        Transform ObjectsFather = BulletFather.transform.Find("Collection-" + type.ToString());
        for (int i = 0; i < ObjectsFather.childCount; i++)
        {
            var current = ObjectsFather.GetChild(i).gameObject;
            if (current.active)
            {
                current.SetActive(false);
                Pool[type].Enqueue(current);
            }
        }
    }

    public GameObject GetInstance(BulletType type)
    {
        if (Pool[type].Count > 0) { 
            GameObject instanceToReuse = Pool[type].Dequeue();
            instanceToReuse.SetActive(true);
            return instanceToReuse;
        }

        return null;
    }

    public void ReturnInstance(BulletType type, GameObject gameObjectToPool)
    {
        Pool[type].Enqueue(gameObjectToPool);
        gameObjectToPool.SetActive(false);
    }


}