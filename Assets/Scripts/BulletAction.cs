using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BulletAction : MonoBehaviour
{
    public ObjectsPool Pool;

    //data part
    public BulletType type;
    public float duration;
    public int harm;
    public int ways;
    public int speed;
    public Vector3 direction;

    void OnEnable()
    {
	    StartCoroutine(DestorySelf());
    }

    void Update()
    {
        transform.Translate(new Vector3(1,0,0) * 5);
    }

    public void InitSelf(ObjectsPool pool)
    {
        duration = 3.0f;
        Pool = pool;

        gameObject.GetComponent<Image>().SetNativeSize();

    }

    IEnumerator DestorySelf()
    {
        yield return new WaitForSeconds(duration);
        Pool.ReturnInstance(gameObject);
    }
}
