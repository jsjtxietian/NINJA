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
    public int speed;
    public Vector3 direction;

    void Start()
    {
        Pool = transform.parent.parent.gameObject.GetComponent<ObjectsPool>();
    }

    void OnEnable()
    {
	    StartCoroutine(DestorySelf());
    }

    public void Init(Vector3 _direction)
    {
        direction = _direction;
    }

    void Update()
    {
        gameObject.GetComponent<RectTransform>().position += transform.right*speed;
    }

    IEnumerator DestorySelf()
    {
        yield return new WaitForSeconds(duration);
        Pool.ReturnInstance(type,gameObject);
    }
}
